export class OrderItem {
    constructor() {}
    orderItemId: number = 0;
    quantity: number = 0;
    unitPrice: number = 0;
    productId: number = 0;
    productCategory: string = '';
    productSize: string = '';
    productTitle: string = '';
    productArtist: string = '';
    productArtId: string = '';
}

export class Order {
    constructor() {}
    orderId: number = 0;
    orderDate: Date = new Date();
    orderNumber: string = Math.random().toString(36).substr(2,5);
    items: Array<OrderItem> = new Array<OrderItem>();

    get subtotal(): number {

        const result = this.items.reduce(
            (tot, val) => {
                return tot + (val.unitPrice * val.quantity);
        }, 0)

        return result;
    }
}