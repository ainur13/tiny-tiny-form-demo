<template>
    <section class="history">
        <h2>Order history</h2>

        <div class="table">
            <input 
                type="search" 
                placeholder="Search..." 
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
                    <td>{{ order.deliveryMethod }}</td>
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
import spinner from './spinner.vue'
import { toDate, formatDate } from '@/utils/date'
import notify from "notify-zh";

interface Order {
    id: number,
    cake: string,
    clientName: string,
    deliveryDate: string,
    deliveryMethod: string,
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
    if(debounce) clearTimeout(debounce);
    debounce = setTimeout(() => { load() }, 300);
})

const fetch = (data : any) => {
    orders.value = [];
    orders.value = data.map(x => ({ id: x.id, ...x.rawJson }))
}

const load = () => {
    const lastRequest = ++requestCounter;
    loading.value = true;
    fetchOrders({ search: search.value })
        .then((data) => fetch(data))
        .finally(() => {
            if(lastRequest === requestCounter)
                loading.value = false
        });
}

const refresh = () => {
    load();
}

const init = () => {
    load();
}

init();

defineExpose({refresh});

</script>

<style scoped>

.history {
    padding: var(--pico-spacing);
}

.table {
    position: relative;
}

</style>