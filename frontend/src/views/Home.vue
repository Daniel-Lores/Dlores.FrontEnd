
<!--<template>
        <div>
          <section class="wrapper">
            <ul class="featured-items">
              <li
                style="border: black 1px solid; width: 400px; display:inline-grid"
                v-for="item in items"
                :key="item.id"
                class="featured-items__item">
                <img class="product-image" :src="item.mainImage" alt="" />
                <p class="product-title">{{ item.name }}</p>
                <p>
                  <em>${{ item.price }}</em>
                </p>
                <b-button><router-link :to="{name: 'details', params: {id: item.id}}">Details</router-link></b-button> ||
                <b-button @click="$router.push('/cart')">Add to cart</b-button>
              </li>
            </ul>
          </section>
        </div>
      </template>-->
<template>
  <div class="home">
    <h1>Listado de Productos</h1>
    <div class="wrapper">
      <ul class="bv-example-row">
        <li style=" display: inline-grid; margin: 10px"  v-for="product in products" :key="product.id">
          <b-card
            :title="product.name"
            :img-src="product.mainImage"
            img-alt="Image"
            img-top
            tag="article"
            style="max-width: 20rem;"
            class="mb-2"
          >
            <!-- <b-card-text>
              {{ product.short }}
            </b-card-text> -->
            <b-card-text> {{ product.price }} € </b-card-text>
            <div style="display: flex">
              <b-button variant="primary" style="margin-left: 10px">
                <router-link
                  :to="{ name: 'Details', params: { id: product.id } }"
                  style="color: white"
                >
                  View Details
                </router-link></b-button
              >
              <b-button variant="primary" @click="addToCart(product.Id, product.name, product.price)">Add to cart</b-button>
              
            </div>
          </b-card>
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
// import HelloWorld from '@/components/HelloWorld.vue'
import api_url from "../utils/api";
export default {
  props: {
    name: String,
    short: String,
    price: Number,
    mainImage: String,
  },
  data() {
    return {
      products: [],
    };
  },
  created() {
    fetch(api_url("/products"))
      .then((result) => result.json())
      .then((data) => (this.products = data));
  },
  methods: {
    addToCart(id, name, price){
        fetch(api_url("/cart/"), {
          method: "POST",
          body: JSON.stringify({
            productId: id,
            productName: name,
            quantity: 1,
            productPrice: price,
          }),
          headers: {
            "Content-Type": "application/json",
          },
        });
    }
  }
};
</script>
