import React from 'react'
import { Header, Text } from 'zmp-ui'

import { useOrderSessionOwner } from '@/modules/orders/use-order-session'
import { clsx } from '@/utils/clsx'

type Props = {
  highlightOwner?: boolean
}

export function OrdersHeader({ highlightOwner }: Props) {

  const owner = useOrderSessionOwner()

  return (
    <Header
      showBackIcon={false}
      title={
        (
          <Text.Title size="small">Thông tin gọi món</Text.Title>
        ) as unknown as string
      }
    />
  )
}
