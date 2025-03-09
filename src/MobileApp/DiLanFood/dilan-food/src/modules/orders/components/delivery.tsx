// import { ElasticTextarea } from "components/elastic-textarea";
import { ListRenderer } from "@/components/list-renderer";
import React, { FC, Suspense, useState } from "react";
import { Box, Icon, Text } from "zmp-ui";
import { TimePicker } from "./time-picker";
import { RequestPersonPickerPhone } from "./person-picker";
import { ListItem } from "@/components/list-item";
import { LocationPicker } from "./location-picker";

export const Delivery: FC = () => {
  //const [note, setNote] = useRecoilState(orderNoteState);

  const [selectedDate, setSelectedDate] = useState<number>(+new Date());
  const [selectedTime, setSelectedTime] = useState<number>(+new Date());

  //const [note, setNote] = useState<string>("");

  return (
    <Box className="space-y-3 mb-32">
      <Text.Header>Hình thức nhận hàng</Text.Header>
      <ListRenderer
        items={[
          {
            left: <Icon icon="zi-location" className="my-auto" />,
            right: (
              <Suspense fallback={<ListItem title="Chọn địa chỉ" subtitle="Chọn địa chỉ nhận hàng" />}>
                <LocationPicker />
              </Suspense>
            ),
          },
          {
            left: <Icon icon="zi-clock-1" className="my-auto" />,
            right: (
              <Box flex className="space-x-2">
                <Box className="flex-1 space-y-[2px]">
                  <Text size="xSmall" className="text-primary font-medium text-md ">
                    Thời gian nhận hàng
                  </Text>
                  <TimePicker
                    date={selectedDate}
                    time={selectedTime}
                    setTime={(time) => setSelectedTime(time)}
                  />
                </Box>
                <Icon icon="zi-chevron-right" />
              </Box>
            ),
          },
          {
            left: <Icon icon="zi-call" className="my-auto" />,
            right: <RequestPersonPickerPhone />,
          },
          // {
          //   left: <Icon icon="zi-note" className="my-auto" />,
          //   right: (
          //     <Box flex>
          //       <ElasticTextarea
          //         placeholder="Nhập ghi chú..."
          //         className="border-none px-0 w-full focus:outline-none"
          //         maxRows={4}
          //         value={note}
          //         onChange={(e) => setNote(e.currentTarget.value)}
          //       />
          //     </Box>
          //   ),
          // },
        ]}
        limit={4}
        renderLeft={(item) => item.left}
        renderRight={(item) => item.right}
      />
    </Box>
  );
};
