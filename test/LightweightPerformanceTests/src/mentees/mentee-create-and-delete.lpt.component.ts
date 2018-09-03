import { BaseLightweightPerformanceTestComponent } from "../common/base.lpt.component";

export class MenteeCreateAndDeleteComponent extends BaseLightweightPerformanceTestComponent {

    async test() {

    }

    async execute() {
        await super._execute(() => this.test(), true);
    }
    
}

customElements.define(`lpt-mentee-create-and-delete`, MenteeCreateAndDeleteComponent);
