import { ShoppingCartItem } from "./shopping-cart-item.model";

export class ShoppingCart {
  public shoppingCartId: string;
  public name: string;
  public shoppingCartItems: ShoppingCartItem[] = [];
}
