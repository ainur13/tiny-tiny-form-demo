<template>
    <section class="history">
        <h2>Order history</h2>

        <div class="table">
            <input 
                type="search" 
                placeholder="Search by Client Name..." 
                v-model="search"
            />

            <table>
                <thead>
                <tr>
                    <th>Cake</th>
                    <th>Client Name</th>
                    <th>Delivery Date</th>
                    <th>Delivery Method</th>
                    <th>Gift Wrap</th>
                </tr>
                </thead>

                <tbody>
                <tr v-for="(order, index) in orders" :key="index">
                    <td>{{ order.cake }}</td>
                    <td>{{ order.clientName }}</td>
                    <td>{{ formatDate(toDate(order.deliveryDate)) }}</td>
                    <td>{{ order.deliveryMethod == 0 ? 'Pickup' : 'Courier' }}</td>
                    <td>{{ order.giftWrap ? 'Yes' : 'No' }}</td>
                </tr>
                </tbody>
            </table>

            <spinner v-if="loading" />
        </div>
    </section>
</template>

<script lang="ts" setup>
import { ref, watch } from 'vue'
import { fetchOrders } from '@/api/order'
import { DeliveryMethod } from '@/models/order'
import spinner from './spinner.vue'
import { toDate, formatDate } from '@/utils/date'

interface Order {
    cake: string,
    clientName: string,
    deliveryDate: string,
    deliveryMethod: DeliveryMethod,
    giftWrap: boolean,
    createdAt: string,
}

const loading = ref(false);

const search = ref('');
let requestCounter = 0;
let debounce: ReturnType<typeof setTimeout> | null = null;

const orders = ref<Order[]>([]);

watch(search, (value) => {
    search.value = value.trim();
    if (!search.value) return;
    if(debounce) clearTimeout(debounce);
    debounce = setTimeout(() => { load() }, 300);
})

const load = () => {
    const lastRequest = ++requestCounter;
    loading.value = true;
    fetchOrders({ clientName: search.value })
        .then((o) => orders.value = o)
        .finally(() => {
            if(lastRequest === requestCounter)
                loading.value = false
        });
}

const init = () => {
    load();
}

init();

defineExpose({load});

</script>

<style scoped>

.history {
    padding: var(--pico-spacing);
}

.table {
    position: relative;
}

</style>