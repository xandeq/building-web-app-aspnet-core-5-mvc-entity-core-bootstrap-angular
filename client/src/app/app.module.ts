import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { Store } from './services/store.service';
import ProductLiewView from './views/productListView.component';
import { CartView } from './views/cartView.component';
import router from './router';
import { ShopPage } from './pages/shopPage.component';
import { CheckoutPage } from './pages/checkout.component';
import { LoginPage } from './pages/loginPage.component';
import { AuthActivator } from './services/authActivator.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
        AppComponent,
        ProductLiewView,
        CartView,
        ShopPage,
        CheckoutPage,
        LoginPage
  ],
  imports: [
      BrowserModule,
      FormsModule,
      HttpClientModule,
      ReactiveFormsModule,
      router
    ],
    providers: [Store, AuthActivator],
  bootstrap: [AppComponent]
})
export class AppModule { }
