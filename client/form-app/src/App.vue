<template>
  <main class="container">
    <order-form @order-accepted="onOrderAccepted"/>
    <order-list ref="list"/>
  </main>
</template>

<script setup lang="ts">

import orderForm from './components/order-form.vue'
import orderList from './components/order-list.vue'
import { ref } from 'vue';

const list = ref();

const onOrderAccepted = () => {
  list.value.refresh();
}

</script>

<style scoped>
.container {
  display: grid;
  grid-template-columns: minmax(250px, 30%) minmax(500px, 70%);
  gap: 10px;                     
  padding: 20px 10px;
}

@media (max-width: 800px) {
  .container {
    grid-template-columns: 1fr;
  }
}
</style>
