"use client"

import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { GetCurrentUser } from "@/lib/actions/auth.action";
import { useAppDispatch, useAppSelector } from "@/lib/store/store";
import { setUser } from "@/lib/store/userSlice";
import Head from "next/head";
import { useEffect, useState } from "react";


export default function Profile() {

  const dispatch = useAppDispatch();
  const user = useAppSelector((state) => state.user.data);

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const user = await GetCurrentUser();
        if (user) {
          dispatch(setUser(user));
        }
      } catch (error) {
        console.error('Failed to fetch user', error);
      }

    };

    fetchUser();
  }, []);

  return (
    <>
      <div className="max-w-4xl mx-auto p-6">
        {/* Profile Card */}
        <Card className="shadow-lg">
          <CardHeader className="text-center">
            <CardTitle className="text-xl font-semibold">User Profile</CardTitle>
          </CardHeader>
          <CardContent className="space-y-6">
            {/* Avatar */}
            <div className="flex justify-center">
              <Avatar>
                {user?.avatarUrl ? (
                  <AvatarImage src={user.avatarUrl} alt="Avatar" />
                ) : (
                  <AvatarFallback>{user?.name || 'S'}</AvatarFallback>
                )}
              </Avatar>
            </div>

            {/* User Info */}
            <div className="space-y-2 text-center">
              <p className="text-lg font-medium">{user?.name}</p>
              <p className="text-sm text-gray-500">Email address: {user?.email}</p>
              <p className="text-sm text-gray-500">Phone number: {user?.phoneNumber}</p>
              <p className="text-sm text-gray-500">Last login: {
                user?.lastLoginAt ?
                  new Date(user.lastLoginAt).toLocaleDateString()
                  : 'Not yet'}</p>
            </div>
          </CardContent>
        </Card>
      </div>
    </>
  );
}