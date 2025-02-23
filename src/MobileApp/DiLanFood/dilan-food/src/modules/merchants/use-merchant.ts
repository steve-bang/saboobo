import { useQuery } from '@tanstack/react-query'
import { getBanners, getMerchantById } from '@/libs/merchant.action'
import { MERCHANT_ID } from '@/constants/common'


export function useMerchant() {
  //const cartActions = useCart((state) => state.actions)
  return useQuery({
    queryKey: ['merchants'],
    queryFn: async () => {

      const res = await getMerchantById(MERCHANT_ID);

      return res
    },
  })
}


export function useMerchantEnableOrder() {
  const { data } = useMerchant()
  return data
}


export function useBanners() {
  //const cartActions = useCart((state) => state.actions)
  return useQuery({
    queryKey: ['banners'],
    queryFn: async () => {

      const res = await getBanners(MERCHANT_ID);

      return res
    },
  })
}