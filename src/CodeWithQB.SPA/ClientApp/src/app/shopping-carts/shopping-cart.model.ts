import { ShoppingCartItem } from "./shopping-cart-item.model";

export class ShoppingCart {
  public shoppingCartId: string;
  public shoppingCartItems: ShoppingCartItem[] = [];
  public version: number;
}
