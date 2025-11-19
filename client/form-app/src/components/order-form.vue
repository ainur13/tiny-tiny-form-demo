<template>
    <section class="card">
        <h1>Order a cake!</h1>

        <div class="form">
            <form @submit.prevent="submit">
                <fieldset>

                    <label for="type">Cake</label>
                    <select id="type" v-model="form.cake" :aria-invalid="ariaInvalid(errors, 'cake')">
                        <option value="" disabled selected hidden></option>
                        <option value="Chocolate">Chocolate</option>
                        <option value="Vanilla">Vanilla</option>
                        <option value="Berry">Berry</option>
                    </select>

                    <label for="name">Name</label>
                    <input id="name" type="text" v-model="form.clientName" :aria-invalid="ariaInvalid(errors, 'clientName')" />


                    <label>Delivery Date
                        <input type="date" v-model="form.deliveryDate" :aria-invalid="ariaInvalid(errors, 'deliveryDate')" />
                    </label>

                    <fieldset>
                        <legend>Delivery Method</legend>
                        <label>
                            <input type="radio" name="delivery" :value="DeliveryMethod.Pickup" v-model="form.deliveryMethod">
                            Pickup
                        </label>
                        <label>
                            <input type="radio" name="delivery" :value="DeliveryMethod.Courier" v-model="form.deliveryMethod">
                            Courier
                        </label>
                    </fieldset>

                    <label>
                        <input type="checkbox" v-model="form.giftWrap" />
                        Gift Wrap
                    </label>
                </fieldset>

                <input type="submit" value="Order" />

            </form>

            <spinner v-if="loading" />
        </div>
    </section>

</template>

<script lang="ts" setup>

import { reactive, ref } from 'vue';
import { ariaInvalid } from '@/utils/form';
import { formatDate } from '@/utils/date';
import { createOrder } from '@/api/order';
import notify from "notify-zh";
import spinner from './spinner.vue';
import { DeliveryMethod } from '@/models/order';

interface OrderForm {
    cake: string,
    clientName: string,
    deliveryDate: string,
    deliveryMethod: DeliveryMethod,
    giftWrap?: boolean,
}

const DefaultOrderForm = <OrderForm>{
    cake: "",
    clientName: "",
    deliveryDate: formatDate(new Date()),
    deliveryMethod: DeliveryMethod.Pickup,
    giftWrap: false,
}

interface ValidationRule {
  rule: (value: any) => boolean
  message: string
}

const emit = defineEmits(["order-accepted"]);

const loading = ref(false);

const form = reactive<OrderForm> ({ ...DefaultOrderForm });

const rules: Record<keyof OrderForm, ValidationRule[]>  = {
    cake: [{ rule: v => !!v, message: "Required field"}],
    clientName: [{ rule: v => !!v, message: "Required field"}],
    deliveryDate: [{ rule: v => !!v, message: "Required field"}],
    deliveryMethod: [{ rule: v => v !== null && v !== undefined, message: "Required field"}],
    giftWrap: [],
};

const errors = reactive<Partial<Record<keyof OrderForm, string>>>({});

const validate = () : boolean => {
    let valid = true;

    for(const k in rules){
        const key = k as keyof OrderForm;

        const fieldRules = rules[key];
        const value = form[key];

        errors[key] = undefined;
        for(const r of fieldRules){
            if(!r.rule(value)){
                errors[key] = r.message;
                valid = false;
                break;
            }
        }
    }

    return valid;
}

const reset = () => {
    Object.assign(form, DefaultOrderForm)
}

const submit = () => {
    if(validate()) {
        loading.value = true;
        createOrder(form)
            .then(() => {
                reset();
                notify.success({
                    message: "Order accepted!",
                    position: 'center-top'
                });
                emit('order-accepted');
            })
            .finally(() => loading.value = false);
    }
}

</script>

<style>

.card {  
    padding: var(--pico-spacing);
    border-radius: var(--pico-border-radius);        
    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.08);
}

.form {
    position: relative;
}

</style>