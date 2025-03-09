import { QueryClient, QueryClientProvider } from '@tanstack/react-query'
import React, { Suspense } from 'react'
import { Route } from 'react-router-dom'
import { AnimationRoutes, App, SnackbarProvider, ZMPRouter } from 'zmp-ui'

import { RootProvider } from './components'
import MerchantInfoPage from './pages/info'
import MerchantRootPage from './pages/menu'
import MerchantOrdersPage from './pages/orders'
import MerchantOrdersViewPage from './pages/orders.view'
import ProfilePage from './pages/profile'
import { SignIn } from './pages/auth/sign-in'
import { SignUp } from './pages/auth/sign-up'
import { MyAddress } from './pages/address/my-address'
import { EditAddress } from './pages/address/edit-address'
import { AddNewAddress } from './pages/address/add-new-address'
import { PickAddress } from './pages/address/pick-address'

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      retry: 2,
    },
  },
})

const MyApp = () => {
  return (
    <App>
      <Suspense>
        <QueryClientProvider client={queryClient}>
          <SnackbarProvider>
            <RootProvider>
              <ZMPRouter>
                <AnimationRoutes>
                  <Route path="/" element={<MerchantRootPage />} />
                  <Route path="/orders" element={<MerchantOrdersPage />} />
                  <Route path="/orders/view" element={<MerchantOrdersViewPage />} />
                  <Route path="/info" element={<MerchantInfoPage />} />
                  <Route path="/profile" element={<ProfilePage />} />
                  <Route path="/sign-in" element={<SignIn />} />
                  <Route path="/sign-up" element={<SignUp />} />
                  <Route path="/my-address" element={<MyAddress />} />
                  <Route path="/edit-address" element={<EditAddress />} />
                  <Route path="/add-new-address" element={<AddNewAddress />} />
                  <Route path="/pick-address" element={<PickAddress />} />
                </AnimationRoutes>
              </ZMPRouter>
            </RootProvider>
          </SnackbarProvider>
        </QueryClientProvider>
      </Suspense>
    </App>
  )
}
export default MyApp
