<template>
  <div class="wrapper">
    <h1>Carrito</h1>
    <section class="wrapper">
      <ul
        style="
          display: inline-grid;
          justify-content: center;
          align-items: center;
          text-align: center;
          margin-top: 70px;
        "
      >
        <li
          v-for="product in products"
          :key="product.id"
          style="
            border: black 3px solid;
            width: window.innerWidth;
            display: flex;
            justify-content: center;
            align-items: center;
            padding-top: 15px;
            margin-bottom: 40px;
          "
        >
          <p>{{ product.productName }}</p>

          <p style="padding-left: 100px; font-weight: bold">Precio:</p>
          <p style="padding-right: 100px">{{ product.productPrice }}€</p>

          <p style="font-weight: bold">Cantidad:</p>
          <p style="padding-right: 100px">{{ product.quantity }}</p>

          <b-button
            @click="
              upgradeQuantity(
                product.id,
                product.quantity,
                product.productName,
                product.productPrice
              )
            "
            style="
              margin-right: 10px;
              background-color: orange;
              color: black;
              margin-bottom: 15px;
            "
            ><b>higher</b></b-button
          >

          <b-button
            @click="
              downgradeQuantity(
                product.id,
                product.quantity,
                product.productName,
                product.productPrice
              )
            "
            style="background-color: cyan; color: black; margin-bottom: 15px"
            ><b>lower</b></b-button
          >

          <b-button
            @click="removeProduct(product.id)"
            style="
              width: 100px;
              color: black;
              font-size: 20px;
              margin-bottom: 15px
              justify-content: center;
              margin-left: 10px;
            "
            ><b>Delete</b></b-button
          >

          <div>
            <p style="margin-left: 10px">
              {{ itemTotal(product.productPrice, product.quantity) }}€
            </p>
          </div>
        </li>
      </ul>

      <div style="width: 200px; height: 100px">
        <p>Total price:{{ totalPrice }}€</p>
      </div>
      <div>
        <router-link
          :to="{ name: 'Checkout' }"
          style="text-decoration: none; color: white"
        >
          <b-button
            style="
              background-color: blue;
              width: 100px;
              height: 100px;
              border-radious: 2px;
            "
            @click="addToOrders"
          >Comprar</b-button
          ></router-link
        >
      </div>
    </section>
  </div>
</template>

<script>
import api_url from "../utils/api";

export default {
  name: "Cart",
  data() {
    return {
      products: [],
    };
  },
  created() {
    fetch(api_url("/cart/"))
      .then((result) => result.json())
      .then((data) => (this.products = data));
  },
  computed: {
    totalPrice: function () {
      let total = 0;
      for (let i = 0; i < this.products.length; i++) {
        total +=
          parseFloat(this.products[i].productPrice) *
          parseFloat(this.products[i].quantity);
      }
      return total;
    },
  },
  methods: {
    upgradeQuantity(id, quantity, name, price) {
      fetch(api_url("/cart/" + id), {
        method: "PUT",
        body: JSON.stringify({
          productId: id,
          quantity: quantity + 1,
          productName: name,
          productPrice: price,
        }),
        headers: {
          "Content-Type": "application/json",
        },
      });
      fetch(api_url("/cart/"))
        .then((result) => result.json())
        .then((data) => (this.products = data));
    },
    downgradeQuantity(id, quantity, name, price) {
      if (quantity > 1) {
        fetch(api_url("/cart/" + id), {
          method: "PUT",
          body: JSON.stringify({
            productId: id,
            quantity: quantity - 1,
            productName: name,
            productPrice: price,
          }),
          headers: {
            "Content-Type": "application/json",
          },
        });
        fetch(api_url("/cart/"))
          .then((result) => result.json())
          .then((data) => (this.products = data));
      } else {
        this.removeProduct(id);
      }
    },
    removeProduct(id) {
      fetch(api_url("/cart/" + id), {
        method: "DELETE",
        body: JSON.stringify({
          productId: id,
        }),
        headers: {
          "Content-Type": "application/json",
        },
      });
      fetch(api_url("/cart/"))
        .then((result) => result.json())
        .then((data) => (this.products = data));
    },
    addToOrders() {
      for (let i = 0; i < this.products.length; i++) {
        fetch(api_url("/orders/"), {
          method: "POST",
          body: JSON.stringify({
            productId: this.products[i].isd,
            productName: this.products[i].productName,
            quantity: this.products[i].quantity,
            productPrice: this.products[i].productPrice,
            totalPriceProduct:
              this.products[i].quantity * this.products[i].productPrice,
          }),
          headers: {
            "Content-Type": "application/json",
          },
        });
      }
      fetch(api_url("/orders/"), {
        method: "POST",
        body: JSON.stringify({
          totalPrice: this.totalPrice,
        }),
        headers: {
          "Content-Type": "application/json",
        },
      });
    },
    itemTotal(productPrice, quantity) {
      return parseFloat(productPrice) * quantity;
    },
  },
};
</script>

<style>
</style>