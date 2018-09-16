import { TemplateResult } from "lit-html";
export declare abstract class BaseLightweightPerformanceTestComponent extends HTMLElement {
    constructor();
    protected _baseUrl: string;
    protected _accessToken: string;
    protected _username: string;
    protected _password: string;
    protected _title: string;
    protected _userId: number;
    protected _autoRun: boolean;
    protected _executing: boolean;
    protected readonly _authenticatedHeaders: {
        "content-type": string;
    } & {
        'Authorization': string;
    };
    protected readonly _loginOptions: {
        username: string;
        password: string;
    };
    protected readonly _anonymousHeaders: {
        "content-type": string;
    };
    protected readonly _styles: any;
    protected abstract execute(): any;
    connectedCallback(): Promise<void>;
    protected readonly template: TemplateResult;
    static readonly observedAttributes: string[];
    protected tryToLogin(options: {
        username: string;
        password: string;
    }): Promise<void>;
    protected _executionTime: number;
    protected _execute(func: {
        (): Promise<any>;
    }, annonymous?: boolean): Promise<void>;
    attributeChangedCallback(name: any, _: any, newValue: any): void;
}
