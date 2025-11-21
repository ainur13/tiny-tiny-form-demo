import api from "./http"

export interface CreateOrderRequest {
    cake: string
    clientName: string
    deliveryDate: string
    deliveryMethod: string
    giftWrap?: boolean
}

export const createOrder = async (request : CreateOrderRequest) => {
    const response = await api.post('/submissions', request);
    return response.data;
}

export interface FetchOrdersRequest {
    search: string,
}

export const fetchOrders = async (request : FetchOrdersRequest) => {
    const response = await api.get('/submissions', { params: request });
    return response.data;
}