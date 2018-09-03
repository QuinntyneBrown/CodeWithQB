import { TemplateResult } from "lit-html";
import { html, render } from "lit-html/lib/lit-extended";
import { unsafeHTML } from "lit-html/lib/unsafe-html";

export abstract class BaseLightweightPerformanceTestComponent extends HTMLElement {
    constructor() {
        super();
        this.execute = this.execute.bind(this);
    }
    
    protected _baseUrl: string;
    protected _accessToken: string;
    protected _username: string;
    protected _password: string;
    protected _title: string;
    protected _userId: number;
    protected _autoRun: boolean = true;
    protected _executing: boolean;
    
    protected get _authenticatedHeaders() { return Object.assign({}, this._anonymousHeaders, { 'Authorization': `Bearer ${this._accessToken}` }); }
    protected get _loginOptions() { return { username: this._username, password: this._password }; }
    protected get _anonymousHeaders() { return { "content-type": "application/json" }; } 
    protected get _styles() { return unsafeHTML(`<style> :host { font-family: Montserrat; }<style>`); }

    protected abstract execute();
    
    async connectedCallback() {
        if (!this.shadowRoot) this.attachShadow({ mode: 'open' });

        render(this.template, this.shadowRoot);    

        if (this._autoRun && !this._executionTime && !this._executing) {
            this._executing = true;
            await this.execute();
        }
    }

    protected get template(): TemplateResult {        
        return html`
            ${this._styles}
            <h1>${this._title}</h1>
            <button hidden?=${this._autoRun} on-click=${this.execute}>run</button>            
            <p hidden?=${!this._executionTime}>Execution Time: ${this._executionTime} milliseconds</p>
        `;
    }
    
    static get observedAttributes() {
        return [
            "auto-run",
            "customer-key",
            "base-url",
            "username",
            "password",
            "title"
        ];
    }
    
    protected async tryToLogin(options: { username: string; password: string }) {
        const response = await fetch(`${this._baseUrl}api/users/signin`, {
            method: 'POST', body: JSON.stringify(options), headers: this._anonymousHeaders
        });

      const json = await response.json();

      this._accessToken = json.accessToken;
      this._userId = json.userId;      
    }


    protected _executionTime: number;
  
    protected async _execute(func: { (): Promise<any> }, annonymous: boolean = false) {

        if (annonymous) {
            const start = Date.now();
            await func();
            this._executionTime = Date.now() - start;
            this.connectedCallback();
        } else {
            await this.tryToLogin(this._loginOptions);

            const start = Date.now();

            await func();

            this._executionTime = Date.now() - start;

            this.connectedCallback();
        }
    }    

    attributeChangedCallback(name, _, newValue) {        
        switch (name) {
            case "auto-run":
                this._autoRun = JSON.parse(newValue);
                break;
            
            case "base-url":
                this._baseUrl = newValue;
                break;

            case "username":
                this._username = newValue;
                break;

            case "password":
                this._password = newValue;
                break;   

            case "title":
                this._title = newValue;
                break;
        }
    }
}
