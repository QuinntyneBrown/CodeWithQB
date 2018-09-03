import { BaseLightweightPerformanceTestComponent } from "../common/base.lpt.component";

export class GetProductsComponent extends BaseLightweightPerformanceTestComponent {

    async getProducts() {
        await fetch(`${this._baseUrl}api/products`);
    }

    async execute() {
        await super._execute(() => this.getProducts(), true);
    }
    
}

customElements.define(`lpt-get-products`, GetProductsComponent);
