import React, { FC, useMemo, useState } from "react";
import { displayDate, displayHalfAnHourTimeRange } from "@/utils/date";
import { matchStatusBarColor } from "@/utils/device";
import { Picker } from "zmp-ui";

// Opening hours: 7:00 - 21:00
const OPENING_HOUR = 7;
const CLOSING_HOUR = 21;

interface TimePickerProps {
  date: number;
  time: number;
  setTime: (time: number) => void;
}

export const TimePicker: FC<TimePickerProps> = ({
  date = 0,
  time = 0,
  setTime,
}) => {

  const availableTimes = useMemo(() => {
    const times: Date[] = [];
    const now = new Date();
    let time = new Date();

    const minutes = Math.ceil(now.getMinutes() / 10) * 10;
    time.setHours(now.getHours());
    time.setMinutes(minutes);
    time.setSeconds(0);
    time.setMilliseconds(0);
    const endTime = new Date();
    endTime.setHours(CLOSING_HOUR);
    endTime.setMinutes(0);
    endTime.setSeconds(0);
    endTime.setMilliseconds(0);
    while (time <= endTime) {
      times.push(new Date(time));
      time.setMinutes(time.getMinutes() + 15);
    }
    return times;
  }, [date]);

  return (
    <Picker
      mask
      maskClosable
      onVisibilityChange={(visbile) => matchStatusBarColor(visbile)}
      inputClass="border-none bg-transparent text-sm m-0 p-0 h-auto"
      placeholder="Chọn thời gian nhận hàng"
      title="Thời gian nhận hàng"
      value={{
        time: availableTimes.find((t) => +t === time)
          ? time
          : +availableTimes[0],
      }}
      formatPickedValueDisplay={({ time }) =>
        time
          ? `${displayHalfAnHourTimeRange(new Date(time.value))}`
          : `Chọn thời gian`
      }
      onChange={({ time }) => {
        if (time) {
          setTime(+time.value);
        }
      }}
      data={[
        {
          options: availableTimes.map((time, i) => ({
            displayName: displayHalfAnHourTimeRange(time),
            value: +time,
          })),
          name: "time",
        }
      ]}
    />
  );
};
