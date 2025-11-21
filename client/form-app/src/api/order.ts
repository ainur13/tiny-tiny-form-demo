import axios from "axios";
import notify from "notify-zh";

const API_BASE_URL = "http://localhost:5166/api/submissions"

export interface CreateOrderRequest {
    cake: string
    clientName: string
    deliveryDate: string
    deliveryMethod: number
    giftWrap?: boolean
}

export const createOrder = async (request : CreateOrderRequest) => {
    try {
        const response = await axios.post(`${API_BASE_URL}`, request);
        return response.data;
    }
    catch (err){
        notify.error({ message: "Ooops! Something went wrong..."})
        throw err
    }
}

export interface FetchOrdersRequest {
    search: string,
}

export const fetchOrders = async (request : FetchOrdersRequest) => {
    try {
        const response = await axios.get(`${API_BASE_URL}`, { params: request });
        return response.data;
    }
    catch (err){
        notify.error({ message: "Ooops! Something went wrong..."})
        throw err
    }
}