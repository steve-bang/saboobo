

### Data
CODE=aEmqGGQJJ4car0TpC85wT-F966eec55ChSHhO0sB152gnsvlEe8SHvthO7qyapvayyzxGKtAUc3vkIfGNVPBQi-p1a9jqKjftfC-TnR7Srt1caz6U-11ADUjU155uJWjYUTF4dwIG0Iwb0KIFijh2eowdo8psuf6xSIU8Y68vW62wQH4BkMtElYxcr8ehTFQjSQhdaAGug6mnV2T0-ZpsfgimgjAYToUakEKrtJgeBc7-RhwG-xOmU2QdDmopzNigU6HasJkrHlPYCfr7fNTL9Ex_pjfbl0ufCV96IEQvMZ2jU90FOFfPA_1aLXYhjO1cOhHI4g2n3-HawWR6foXVUkGc14goPPtXef46np37G3MbLCaLCbjJyMACMT6ycbemeTHR6_iF6R_hfM9PEhlcTUJwRZdq86mSOY8-uBTjTTzcwo4j_oJWWAmeBlXXEp7GeAWdV6lfiyco4RaUHIXN4a
OA_ID=2298264073013244343
APP_ID=2739215765323395042

### Gửi thông báo cho người dùng

https://mini.zalo.me/miniapp/3147492405195201163/setting/notification

#### 1. Get access token
curl \
-X POST \
-H 'Content-Type: application/x-www-form-urlencoded' \
-H 'secret_key: FvVSPLfPbCFSkVsVUtTw' \
--data-urlencode 'code=aEmqGGQJJ4car0TpC85wT-F966eec55ChSHhO0sB152gnsvlEe8SHvthO7qyapvayyzxGKtAUc3vkIfGNVPBQi-p1a9jqKjftfC-TnR7Srt1caz6U-11ADUjU155uJWjYUTF4dwIG0Iwb0KIFijh2eowdo8psuf6xSIU8Y68vW62wQH4BkMtElYxcr8ehTFQjSQhdaAGug6mnV2T0-ZpsfgimgjAYToUakEKrtJgeBc7-RhwG-xOmU2QdDmopzNigU6HasJkrHlPYCfr7fNTL9Ex_pjfbl0ufCV96IEQvMZ2jU90FOFfPA_1aLXYhjO1cOhHI4g2n3-HawWR6foXVUkGc14goPPtXef46np37G3MbLCaLCbjJyMACMT6ycbemeTHR6_iF6R_hfM9PEhlcTUJwRZdq86mSOY8-uBTjTTzcwo4j_oJWWAmeBlXXEp7GeAWdV6lfiyco4RaUHIXN4a' \
--data-urlencode 'app_id=2739215765323395042' \
--data-urlencode 'grant_type=authorization_code' \
'https://oauth.zaloapp.com/v4/oa/access_token'

#### 2. Gửi tin giao dịch

URL: https://developers.zalo.me/docs/official-account/tin-nhan/tin-giao-dich/gui-tin-giao-dich

```
curl --location --request GET 'https://openapi.zalo.me/v3.0/oa/message/transaction' \
--header 'access_token: DZ-yJyo0OaGmCRXBoeLdGZeskqp1dXK-UKcCIEE0MH4QLenYjvel7njRZKUdg2Gk25Zv9xViLrSrFjecZOq6HGiygH2jz10Q4IhB5xdf7tm17yDrkSOPFmKvdZA4p4iwVIRoSEtmJ0519U8AiT4w2Jyddbt2nIS6VWUXNjsiVYDrUiDttvTiAIv_mo2Zis59Mc3CDCIDOMz9Nk8VvOixRsW-X1Bip5vZSYY7E8wRBcekJO4nXO8XFYLMbpA_j3Hx8NVl9OpSEcyH494nczGiKMO7y5_vioXJT0U32kh8Ebjd59yEd_P60MP9nsdCdWPYNs287UMa0qrkRBqZbA8-D71IY4pgeXSC4NM3G-wi625YNe54wRaoBsfwvsoGg64P5t6WJxUkEG8dVOrtkvOW8XmbZa9GLyc6x3hBbs92' \
--header 'Content-Type: application/json' \
--data '{
    "recipient": {
        "user_id": "7279905758396907775"
    },
    "message": {
        "attachment": {
            "type": "template",
            "payload": {
                "template_type": "transaction_order",
                "language": "VI",
                "elements": [
                    {
                        "attachment_id": "a-JJEvLdkcEPxTOwb6gYTfhwm26VSBHjaE3MDfrWedgLyC0smJRiA8w-csdGVg1cdxZLPT1je7k4i8nwbdYrSCJact3NOVGltEUQTjDayIhTvf1zqsR-Ai3aboRERgjvm-cI8iqv-NoIxi0cdNBoE6SYVJooM6xKTBft",
                        "type": "banner"
                    },
                      {
                        "type": "header",
                        "content": "Trạng thái đơn hàng",
                        "align": "left"
                    },
                    {
                        "type": "text",
                        "align": "left",
                        "content": "• Cảm ơn bạn đã mua hàng tại cửa hàng.<br>• Thông tin đơn hàng của bạn như sau:"
                    },
                    {
                        "type": "table",
                        "content": [
                            {
                                "value": "F-01332973223",
                                "key":"Mã khách hàng"
                            },
                            {
                                "style": "yellow",
                                "value": "Đang giao",
                                "key": "Trạng thái"
                            },
                            {
                                "value": "250,000đ",
                                "key": "Giá tiền"
                            }
                        ]
                    },
                    {
                        "type": "text",
                        "align": "center",
                        "content": "📱Lưu ý điện thoại. Xin cảm ơn!"
                    }
                ],
                "buttons": [
                    {
                        "title": "Kiểm tra lộ trình - default icon",
                        "image_icon": "",
                        "type": "oa.open.url",
                        "payload": {
                            "url": "https://oa.zalo.me/home"
                        }
                    },
                    {
                        "title": "Xem lại giỏ hàng",
                        "image_icon": "wZ753VDsR4xWEC89zNTsNkGZr1xsPs19vZF22VHtTbxZ8zG9g24u3FXjZrQvQNH2wMl1MhbwT5_oOvX5_szXLB8tZq--TY0Dhp61JRfsAWglCej8ltmg3xC_rqsWAdjRkctG5lXzAGVlQe9BhZ9mJcSYVIDsc7MoPMnQ",
                        "type": "oa.query.show",
                        "payload": "kiểm tra giỏ hàng"
                    }
                    ,
                    {
                        "title": "Liên hệ tổng đài",
                        "image_icon": "gNf2KPUOTG-ZSqLJaPTl6QTcKqIIXtaEfNP5Kv2NRncWPbDJpC4XIxie20pTYMq5gYv60DsQRHYn9XyVcuzu4_5o21NQbZbCxd087DcJFq7bTmeUq9qwGVie2ahEpZuLg2KDJfJ0Q12c85jAczqtKcSYVGJJ1cZMYtKR",
                        "type": "oa.open.phone",
                        "payload": {
                            "phone_code":"84123456789"
                        }
                    }
                ]
            }
        }
    }
}'
``` 

Notes


Cho tự chọn thời gian nhận hàng. 
Chỉ chọn ngày thôi.

Tích điểm, đặt 1 phần sẽ tích được điểm. 

giao diện Đặt hàng 

Mục tin tức, thì chỉ cho xem. 

Lịch sử thì sẽ có bao nhiêu? danh sách, số lượng, ngày đặt, số tiền

Cấu hình trên web về trạng thái quán ăn. 