'use client'

import { useState, FormEvent } from "react";
import { Button } from "@/components/ui/button";
import { signIn } from "@/lib/actions/auth.action";
import { Spinner } from "@/components/ui/spinner";
import { redirect, useRouter } from "next/navigation";
import { useMerchantContext } from "@/lib/MerchantContext";
import { getMerchantsByUserLogged } from "@/lib/actions/merchant.action";

interface FormData {
  phoneNumber: string;
  password: string;
}

export default function SignIn() {

  const [formData, setFormData] = useState<FormData>({ phoneNumber: "", password: "" });
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState<boolean>(false);

  const { setMerchant } = useMerchantContext();

  const router = useRouter();

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();

    // Form validation
    if (!formData.phoneNumber || !formData.password) {
      setError("Phone number and password are required.");
      return;
    }

    setLoading(true);

    try {
      await signIn(formData);

      // Save merchant user sign in success
      handleSaveMerchantUserSignInSuccess();


      router.push("/profile");
    } catch (error) {
      setError("Your phone number or password is incorrect.");
    }
    setLoading(false);

  };

  const handleSaveMerchantUserSignInSuccess = async () => {
    const merchant = await getMerchantsByUserLogged();
    setMerchant(merchant);
  }

  return (
    <div className="flex items-center justify-center min-h-screen bg-gray-50">
      <div className="w-full max-w-md p-8 bg-white rounded-lg shadow-md">
        <h2 className="text-3xl font-bold text-center text-gray-800">Sign In</h2>

        {error && <p className="text-red-500 text-center">{error}</p>}

        <form onSubmit={handleSubmit} className="mt-6">
          <div className="mb-4">
            <label htmlFor="email" className="block text-gray-700">Phone Number</label>
            <input
              type="text"
              id="phoneNumber"
              name="phoneNumber"
              className="w-full px-4 py-2 mt-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
              value={formData.phoneNumber}
              onChange={handleChange}
              placeholder="Enter your phone number"
            />
          </div>

          <div className="mb-6">
            <label htmlFor="password" className="block text-gray-700">Password</label>
            <input
              type="password"
              id="password"
              name="password"
              className="w-full px-4 py-2 mt-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
              value={formData.password}
              onChange={handleChange}
              placeholder="Enter your password"
            />
          </div>
          {
            loading ? (
              <Button type="submit" className="w-full rounded-lg" disabled>
                <Spinner />
              </Button>
            ) : (
              <Button type="submit" className="w-full px-4 rounded-lg">
                Sign In
              </Button>
            )
          }
        </form>
      </div>
    </div>
  );
};

