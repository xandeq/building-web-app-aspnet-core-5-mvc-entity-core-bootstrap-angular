import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map, Observable, tap } from "rxjs";
import { LoginRequest, LoginResults } from "../shared/LoginResults";
import { Order, OrderItem } from "../shared/Order";
import { Product } from "../shared/Product";

@Injectable()
export class Store {

    constructor(private http: HttpClient) { }

    public products: Product[] = [];
    public order: Order = new Order();
    public token = "";
    public expiration = new Date();

    loadProducts(): Observable<void> {
        return this.http.get<Product[]>("/api/products")
            .pipe(
                tap(data => console.log(data)),
                map(data => { this.products = data; return; })
            );
    }

    get loginRequired(): boolean {
        return this.token.length === 0 || this.expiration > new Date();
    }

    login(creds: LoginRequest) {
        return this.http.post<LoginResults>("/account/CreateToken", creds)
            .pipe(
                map((data) => {
                    this.token = data.token;
                    this.expiration = data.expiration;
                })
            )
    }

    checkout() {
        const headers = new HttpHeaders().set("Authorization", `Bearer ${this.token}`)
        return this.http.post("/api/orders", this.order, { headers: headers })
            .pipe(
                map(() => { this.order = new Order() }));
    }

    public addToOrder(product: Product) {

        let item: OrderItem = new OrderItem();

        if (this.order) {
            item = this.order.items.find(i => i.productId === product.id)!;
        }

        if (item) {
            item.quantity++;
        } else {
            item = new OrderItem();

            item.productId = product.id;
            item.productArtist = product.artist;
            item.productCategory = product.category;
            item.productArtId = product.artId;
            item.productTitle = product.title;
            item.productSize = product.size;
            item.unitPrice = product.price;
            item.quantity = 1;
            this.order.items.push(item);
        }
    }
}