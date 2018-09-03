import { BaseLightweightPerformanceTestComponent } from "../common/base.lpt.component";

export class GetProductsComponent extends BaseLightweightPerformanceTestComponent {

    async getProducts() {
        await fetch("http://localhost:51578/api/products");
    }

    async execute() {
        await super._execute(() => this.getProducts(),true);
    }
    
}

customElements.define(`lpt-get-products`, GetProductsComponent);
