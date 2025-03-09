// import { produce } from 'immer'
// import { create, useStore } from 'zustand'
// import { persist } from 'zustand/middleware'

// import { storage } from '@/utils/storage'
// import { IProductType, IToppingType } from '@/types/product'
// import { IShippingAddress } from '@/types/cart'
// import { createCart } from '@/libs/cart.action'


// export type CartItem = {
//   id: string
//   product: IProductType
//   toppings?: IToppingType[]
//   note?: string
//   quantity: number
//   price: number
//   total: number // = price * quantity
//   createdAt: number
// }

// type Cart = {
//   id?: string
//   items: Record<string, CartItem>
//   totalPrice: number
//   createdAt?: number
//   shippingAddress?: IShippingAddress,
//   note?: string
//   estimatedDeliveryDateFrom?: string
//   estimatedDeliveryDateTo?: string
//   actions: {
//     add: (payload: { product: IProductType; toppings?: IToppingType[] }) => void
//     remove: (id: CartItem['id']) => void
//     increase: (id: CartItem['id']) => void
//     decrease: (id: CartItem['id']) => void
//     updateNote: (id: CartItem['id'], note: string) => void
//     clear: () => void
//     productsUpdated: (products: IProductType[]) => void,
//     updateShippingAddress: (shippingAddress: IShippingAddress) => void,
//     updatePhoneNumber: (phoneNumber: string) => void,
//     updateAddressDetail: (addressDetail: string) => void,
//     updateEstimatedDeliveryDate: (from: string, to: string) => void,
//   }
// }

// function buildCartItemId(product: IProductType, toppings: IToppingType[] = []) {
//   const ids = toppings.map((t) => t.id).toSorted()
//   return `${product.id}:${ids.join()}`
// }

// function calcTotal(items: CartItem[]) {
//   return items.reduce((acc, item) => acc + item.total, 0)
// }

// export const useCart = create(
//   persist<Cart>(
//     (set) => ({

//       items: {},
//       totalPrice: 0,
//       actions: {
//         add: ({ product, toppings }) => {
//           set(
//             produce<Cart>((state) => {
//               const id = buildCartItemId(product, toppings)
//               const item = state.items[id]
//               const quantity = (item?.quantity || 0) + 1
//               const toppingsTotal = toppings?.reduce((acc, t) => acc + t.price, 0) || 0
//               const price = product.price + toppingsTotal
//               const total = price * quantity
//               state.items[id] = {
//                 id,
//                 product,
//                 toppings,
//                 quantity,
//                 price,
//                 total,
//                 createdAt: Date.now(),
//               }
//               state.totalPrice = calcTotal(Object.values(state.items))
//             }),
//           )

//           // Call api to update cart
//         },
//         remove: (id) =>
//           set(
//             produce<Cart>((state) => {
//               if (!state.items[id]) return
//               delete state.items[id]
//               state.totalPrice = calcTotal(Object.values(state.items))
//             }),
//           ),
//         increase: (id) =>
//           set(
//             produce<Cart>((state) => {
//               if (!state.items[id]) return
//               state.items[id].quantity += 1
//               state.items[id].total = state.items[id].price * state.items[id].quantity
//               state.totalPrice = calcTotal(Object.values(state.items))
//             }),
//           ),
//         decrease: (id) =>
//           set(
//             produce<Cart>((state) => {
//               if (!state.items[id]) return
//               if (state.items[id].quantity === 1) {
//                 delete state.items[id]
//                 state.totalPrice = calcTotal(Object.values(state.items))
//                 return
//               }
//               state.items[id].quantity -= 1
//               state.items[id].total = state.items[id].price * state.items[id].quantity
//               state.totalPrice = calcTotal(Object.values(state.items))
//             }),
//           ),
//         updateNote: (id, note) =>
//           set(
//             produce<Cart>((state) => {
//               if (!state.items[id]) return
//               state.items[id].note = note
//             }),
//           ),
//         clear: () => set({ items: {}, totalPrice: 0 }),
//         productsUpdated: (products: IProductType[]) => {
//           // Tính toán lại giá trị của các item trong giỏ hàng khi Menu được cập nhật
//           set(
//             produce<Cart>((state) => {
//               const newItems: Record<string, CartItem> = {}
//               const productsMap = products.reduce((acc, p) => ({ ...acc, [p.id]: p }), {} as Record<string, IProductType>)
//               Object.values(state.items).forEach((item) => {
//                 const product = productsMap[item.product.id]
//                 if (!product) return
//                 const toppingsTotal = item.toppings?.reduce((acc, t) => acc + t.price, 0) || 0
//                 const price = product.price + toppingsTotal
//                 const total = price * item.quantity
//                 newItems[item.id] = {
//                   ...item,
//                   product,
//                   price,
//                   total,
//                 }
//               })
//               state.items = newItems
//               state.totalPrice = calcTotal(Object.values(state.items))
//             }),
//           )
//         },
//         updateShippingAddress: (shippingAddress) =>
//           set(
//             produce<Cart>((state) => {
//               state.shippingAddress = shippingAddress
//             }),
//           ),
//         updatePhoneNumber: (phoneNumber) =>
//           set(
//             produce<Cart>((state) => {
//               // if shippingAddress is not exist, create new one with phoneNumber
//               if (!state.shippingAddress) {
//                 state.shippingAddress = { phoneNumber: phoneNumber, name: '', addressDetail: '' }
//                 return
//               }
//               state.shippingAddress.phoneNumber = phoneNumber
//             }),
//           ),
//         updateAddressDetail: (addressDetail) =>
//           set(
//             produce<Cart>((state) => {
//               if (!state.shippingAddress) {
//                 state.shippingAddress = { phoneNumber: '', name: '', addressDetail: addressDetail }
//                 return
//               }
//               state.shippingAddress.addressDetail = addressDetail
//             }),
//           ),
//         updateEstimatedDeliveryDate: (from, to) =>
//           set(
//             produce<Cart>((state) => {
//               state.estimatedDeliveryDateFrom = from
//               state.estimatedDeliveryDateTo = to
//             }
//             )
//           )
//       },
//     }),
//     {
//       name: 'cart',
//       storage: storage,
//       partialize: (state) => {
//         const now = Date.now()
//         const createdAt = new Date(state.createdAt || now)
//         // Tự động reset cart sau mỗi 12h
//         if (now - createdAt.getTime() > 12 * 60 * 60 * 1000) {
//           return { items: {}, totalPrice: 0, createdAt: now } as Cart
//         }
//         return {
//           items: state.items,
//           totalPrice: state.totalPrice,
//           createdAt: createdAt.getTime(),
//         } as Cart
//       },
//       onRehydrateStorage: () => async (state) => {
//         if (state) {
//           try {
//             const currentCart = await createCart();
//             if (currentCart.data) {
//               state.id = currentCart.data.id;
//             }

//           } catch (error) {
//             console.error('Failed to fetch current cart:', error);
//           }
//         }
//       },
//     },
//   ),
// )

// export const useTotalCartItems = () =>
//   useCart((state) => Object.values(state.items).reduce((acc, item) => acc + item.quantity, 0))

// export function userShippingAddressCart() {
//   return useStore(useCart, (state) => state.shippingAddress)
// }



// =========================================================


import { produce } from 'immer'
import { create, useStore } from 'zustand'
import { persist } from 'zustand/middleware'

import { storage } from '@/utils/storage'
import { IProductType, IToppingType } from '@/types/product'
import { ICartItem, IShippingAddress } from '@/types/cart'
import { addItemToCart, createCart, removeItemToCart, updateItemsToCart } from '@/libs/cart.action'



type Cart = {
  id: string
  items: ICartItem[] | []
  totalPrice: number
  createdAt?: number
  shippingAddress?: IShippingAddress,
  note?: string
  estimatedDeliveryDateFrom?: string
  estimatedDeliveryDateTo?: string
  actions: {
    init: (accessToken: string) => void
    add: (accessToken: string, payload: { product: IProductType; toppings?: IToppingType[] }) => Promise<void>
    remove: (accessToken: string, id: string) => void
    increase: (accessToken: string, id: string) => void
    decrease: (accessToken: string, id: string) => void
    updateNote: (accessToken: string, id: string, note: string) => void
    clear: () => void
    productsUpdated: (products: IProductType[]) => void,
    updateShippingAddress: (shippingAddress: IShippingAddress) => void,
    updatePhoneNumber: (phoneNumber: string) => void,
    updateAddressDetail: (addressDetail: string) => void,
    updateEstimatedDeliveryDate: (from: string, to: string) => void,
  }
}

export const useCart = create(
  persist<Cart>(
    (set, get) => ({
      id: '',
      items: [],
      totalPrice: 0,
      actions: {
        init: async (accessToken) => {

          const { id } = get();

          let cartId = id;

          // If cartId doesn't exist, create a new cart
          if (!cartId) {
            const currentCart = await createCart(accessToken);

            cartId = currentCart.data?.id ?? '';
            set({ id: cartId }); // Save the new cartId in the state
          }
          // Call api to update cart
        },
        add: async (accessToken, { product, toppings }) => {

          const { id } = get();

          let cartId = id;

          // If cartId doesn't exist, create a new cart
          if (!cartId) {
            const currentCart = await createCart(accessToken);

            console.log('currentCart', currentCart);

            cartId = currentCart.data?.id ?? '';
            set({ id: cartId }); // Save the new cartId in the state
          }


          if (cartId) {
            const addItemRes = await addItemToCart(accessToken, cartId, { productId: product.id, productName: product.name, productImage: product.urlImage, quantity: 1, price: product.price });

            // Update the state with the response data
            set(
              produce<Cart>((state) => {
                // Assuming `addItemRes` contains the updated cart data
                state.id = addItemRes.data?.id ?? ''; // Update cart id
                state.items = addItemRes.data?.items ?? []; // Update items
                state.totalPrice = addItemRes.data?.totalPrice ?? 0// Update total price
              }))
          }
          // Call api to update cart
        },
        remove: async (accessToken, idItem) => {

          const { id } = get();

          console.log('id', id);

          const res = await removeItemToCart(accessToken, id, [idItem]);

          console.log('res', res);

          // Update the state with the response data
          set(
            produce<Cart>((state) => {
              // Assuming `addItemRes` contains the updated cart data
              state.items = res.data?.items ?? []; // Update items
              state.totalPrice = res.data?.totalPrice ?? 0// Update total price
            }))
        },
        increase: async (accessToken, itemId) => {

          const { id, items } = get();

          const item = items.find(i => i.id === itemId);

          if (!item) return;

          const newQuantity = item.quantity + 1;

          const newItems = items.map(i => i.id === itemId ? { ...i, quantity: newQuantity } : i)
            .map(i => ({ productId: i.productId, productName: i.productName, productImage: i.productImage, quantity: i.quantity, price: i.unitPrice }));

          const updateItemsCart = await updateItemsToCart(accessToken, id, newItems);

          set(
            produce<Cart>((state) => {
              // Assuming `addItemRes` contains the updated cart data
              state.items = updateItemsCart.data?.items ?? []; // Update items
              state.totalPrice = updateItemsCart.data?.totalPrice ?? 0// Update total price
            }))
        },
        decrease: async (accessToken, itemId) => {

          const { id, items } = get();

          const item = items.find(i => i.id === itemId);

          if (!item) return;

          const newQuantity = item.quantity - 1;

          const newItems = items.map(i => i.id === itemId ? { ...i, quantity: newQuantity } : i)
            .filter(i => i.quantity > 0)
            .map(i => ({ productId: i.productId, productName: i.productName, productImage: i.productImage, quantity: i.quantity, price: i.unitPrice }));

          const updateItemsCart = await updateItemsToCart(accessToken, id, newItems);

          set(
            produce<Cart>((state) => {
              // Assuming `addItemRes` contains the updated cart data
              state.items = updateItemsCart.data?.items ?? []; // Update items
              state.totalPrice = updateItemsCart.data?.totalPrice ?? 0// Update total price
            }))
        },
        updateNote: async (accessToken, itemId, notes) => {

          const { id, items } = get();

          const item = items.find(i => i.id === itemId);

          if (!item) return;


          const newItems = items.map(i => i.id === itemId ? { ...i, note: notes } : i)
            .map(i => ({ productId: i.productId, productName: i.productName, productImage: i.productImage, quantity: i.quantity, price: i.unitPrice, notes: i.notes }));

          console.log('newItems', newItems);

          const updateItemsCart = await updateItemsToCart(accessToken, id, newItems);

          set(
            produce<Cart>((state) => {
              // Assuming `addItemRes` contains the updated cart data
              state.items = updateItemsCart.data?.items ?? []; // Update items
              state.totalPrice = updateItemsCart.data?.totalPrice ?? 0// Update total price
            }))
        },
        clear: () => console.log('clear'),
        productsUpdated: (products: IProductType[]) => console.log('productsUpdated'),
        //   {
        //   // Tính toán lại giá trị của các item trong giỏ hàng khi Menu được cập nhật
        //   set(
        //     produce<Cart>((state) => {
        //       const newItems: Record<string, CartItem> = {}
        //       const productsMap = products.reduce((acc, p) => ({ ...acc, [p.id]: p }), {} as Record<string, IProductType>)
        //       Object.values(state.items).forEach((item) => {
        //         const product = productsMap[item.product.id]
        //         if (!product) return
        //         const toppingsTotal = item.toppings?.reduce((acc, t) => acc + t.price, 0) || 0
        //         const price = product.price + toppingsTotal
        //         const total = price * item.quantity
        //         newItems[item.id] = {
        //           ...item,
        //           product,
        //           price,
        //           total,
        //         }
        //       })
        //       state.items = newItems
        //       state.totalPrice = calcTotal(Object.values(state.items))
        //     }),
        //   )
        // },
        updateShippingAddress: (shippingAddress) =>
          set(
            produce<Cart>((state) => {
              state.shippingAddress = shippingAddress
            }),
          ),
        updatePhoneNumber: (phoneNumber) =>
          set(
            produce<Cart>((state) => {
              // if shippingAddress is not exist, create new one with phoneNumber
              if (!state.shippingAddress) {
                state.shippingAddress = { phoneNumber: phoneNumber, name: '', addressDetail: '' }
                return
              }
              state.shippingAddress.phoneNumber = phoneNumber
            }),
          ),
        updateAddressDetail: (addressDetail) =>
          set(
            produce<Cart>((state) => {
              if (!state.shippingAddress) {
                state.shippingAddress = { phoneNumber: '', name: '', addressDetail: addressDetail }
                return
              }
              state.shippingAddress.addressDetail = addressDetail
            }),
          ),
        updateEstimatedDeliveryDate: (from, to) =>
          set(
            produce<Cart>((state) => {
              state.estimatedDeliveryDateFrom = from
              state.estimatedDeliveryDateTo = to
            }
            )
          )
      },
    }),
    {
      // Save cart data to local storage with the key 'cart'
      name: 'cart',
      storage: storage,
      partialize: (state) => {
        const now = Date.now()
        const createdAt = new Date(state.createdAt || now)
        // Tự động reset cart sau mỗi 12h
        if (now - createdAt.getTime() > 12 * 60 * 60 * 1000) {
          return { items: {}, totalPrice: 0, createdAt: now } as Cart
        }
        return {
          items: state.items,
          totalPrice: state.totalPrice,
          createdAt: createdAt.getTime(),
        } as Cart
      },
    },
  ),
)

export const useTotalCartItems = () =>
  useCart((state) => Object.values(state.items).reduce((acc, item) => acc + item.quantity, 0))

export function userShippingAddressCart() {
  return useStore(useCart, (state) => state.shippingAddress)
}

