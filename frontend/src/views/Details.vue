<template>
  <ul class="bv-example-row">
    <li style="display: inline-grid; margin: 10px">
      <b-card
        :title="product.name"
        :img-src="product.mainImage"
        img-alt="Image"
        img-top
        tag="article"
        style="max-width: 20rem"
        class="mb-2"
      >
        <b-card-text>
          {{ product.description }}
        </b-card-text>
        <b-card-text> {{ product.price }} € </b-card-text>
        <div>
          <b-button variant="primary" @click="addToCart">Add to cart</b-button>
        </div>
      </b-card>
    </li>
  </ul>
</template>

<script>
import api_url from "../utils/api";

export default {
  name: "Details",
  data() {
    return {
      product: {},
    };
  },
  created() {
    this.getProductDetails(this.$route.params.id);
  },
  beforeRouteUpdate(to, from) {
    this.getProductDetails(to.params.id);
  },
  methods: {
    addToCart(){
        fetch(api_url("/cart/"), {
          method: "POST",
          body: JSON.stringify({
            productId: this.product.Id,
            productName: this.product.name,
            quantity: 1,
            productPrice: this.product.price,
          }),
          headers: {
            "Content-Type": "application/json",
          },
        });
    },
    getProductDetails(id) {
      fetch(api_url("/products/" + id))
        .then((result) => result.json())
        .then((data) => (this.product = data));
    },
  }
};
</script>

<style>
</style>