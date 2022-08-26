import { Component, OnInit } from "@angular/core";
import { Store } from "../services/store.service";

@Component({
    selector: "product-list",
    templateUrl: "productListView.component.html",
    styleUrls: ["productListView.component.css"]
})

export default class ProductLiewView implements OnInit {
    constructor(public store: Store) {
    }
    ngOnInit(): void {
        this.store.loadProducts()
            .subscribe();
    }
}