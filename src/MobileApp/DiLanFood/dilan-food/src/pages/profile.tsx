import React from 'react'
import { Page } from 'zmp-ui'

import { PageContainer } from '@/components'
import { MerchantLayout, MerchantTabs } from '@/modules/merchants/components'
import Profile from '@/modules/profile/components/profile-page'

export default function ProfilePage() {
    return (
        <MerchantLayout>
            <Page restoreScroll>
                <PageContainer withBottomNav>
                    <Profile />
                </PageContainer>
            </Page>
            <MerchantTabs activeTab="profile" />
        </MerchantLayout>
    )
}
