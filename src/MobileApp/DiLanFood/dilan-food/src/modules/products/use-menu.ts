// import { useQuery } from '@tanstack/react-query'

// import { request } from '@/utils/request'

// import { useCart } from '../orders/use-cart'

// export type Category = { 
//   id: number
//   name: string
// }

// export type Topping = {
//   id: number
//   name: string
//   price: number
// }

// export type Product = {
//   id: number
//   name: string
//   description: string
//   price: number
//   imageUrl: string
//   toppings?: Topping[]
// }

// export type MenuItem = {
//   category: Category
//   products: Product[]
// }

// export function useMenu() {
//   const actions = useCart((state) => state.actions)
//   return useQuery({
//     queryKey: ['menu'],
//     queryFn: async () => {
//       const res = await request<MenuItem[]>(`/menu-items`)
//       const menuItems = res?.filter((item) => item.products.length > 0)
//       const products = menuItems?.flatMap((item) => item.products)
//       actions.productsUpdated(products)
//       return menuItems
//     },
//   })
// }

import { useQuery } from '@tanstack/react-query'

import { useCart } from '../orders/use-cart'
import { ICategoryType } from '@/types/category'
import { IProductType } from '@/types/product'
import { getCategories } from '@/libs/category.action'
import { MERCHANT_ID } from '@/constants/common'
import { getProducts } from '@/libs/product.action'
import { useMerchant } from '../merchants/use-merchant'



export type MenuItem = {
  category: ICategoryType
  products: IProductType[]
}

export function useMenu() {
  const actions = useCart((state) => state.actions)

  return useQuery({
    queryKey: ['menu'],
    queryFn: async () => {

      const categories = await getCategories(MERCHANT_ID);

      // Loop through categories and get products for each category

      if (!categories) return null

      const products = await getProducts({ merchantId: MERCHANT_ID });

      // map products to categories
      const res = categories.map((category) => {
        const categoryProducts = products?.filter((product) => product.categoryId === category.id)
        return {
          category: category,
          products: categoryProducts || []
        }
      })

      //actions.productsUpdated(products)

      return res.filter((item) => item.products.length > 0);
    },
  })
}

