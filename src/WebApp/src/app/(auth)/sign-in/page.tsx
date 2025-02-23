'use client'

import { useState, FormEvent } from "react";
import { Button } from "@/components/ui/button";
import { checkPhoneNumberExist, GetCurrentUser, signIn } from "@/lib/actions/auth.action";
import { Spinner } from "@/components/ui/spinner";
import { useRouter } from "next/navigation";
import { getMerchantsByUserLogged } from "@/lib/actions/merchant.action";
import { useAppDispatch } from "@/lib/store/store";
import { setMerchant } from "@/lib/store/merchantSlice";
import { setUser } from "@/lib/store/userSlice";
import PhoneInput from 'react-phone-number-input'
import 'react-phone-number-input/style.css'

interface FormData {
  phoneNumber: string;
  password: string;
}

enum AuthSteps {
  InputPhoneNumber,
  InputPassword
}

export default function SignIn() {
  const [formData, setFormData] = useState<FormData>({ phoneNumber: "", password: "" });
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState<boolean>(false);
  const [authStep, setAuthStep] = useState<AuthSteps>(AuthSteps.InputPhoneNumber);

  const dispatch = useAppDispatch();

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
      await signIn({
        phoneNumber: formData.phoneNumber,
        password: formData.password
      });

      // Save merchant user sign in success
      await handleDispatchDataAfterSignInSuccess();

      router.push("/merchant");
    } catch {
      setError("Your phone number or password is incorrect.");
    }
    setLoading(false);
  };

  const handleDispatchDataAfterSignInSuccess = async () => {
    const user = await GetCurrentUser();
    if (user) dispatch(setUser(user));

    const merchant = await getMerchantsByUserLogged();
    dispatch(setMerchant(merchant));
  }

  const onClickNext = async () => {
    if (!formData.phoneNumber) {
      setError("Phone number is required.");
      return;
    }

    // Check phone number is valid regex
    if (!formData.phoneNumber.match(/^\+84\d{9,10}$/)) {
      setError("Phone number is invalid.");
      return;
    }
    const isPhoneNumberExists = await checkPhoneNumberExistSystem();

    if (isPhoneNumberExists){
      setAuthStep(AuthSteps.InputPassword);
      setError(null);
    }

  }

  const checkPhoneNumberExistSystem = async () => {

    try {
      setLoading(true);
      // Check phone number exist in system
      const phoneNumber = await checkPhoneNumberExist(formData.phoneNumber);

      if (!phoneNumber) {
        setError("Phone number is not exist.");
        return false;
      }
    }
    catch {
      setError("Phone number is not exist.");
    }
    finally {
      setLoading(false);
    }

    return true;
  }

  return (
    <div className="flex items-center justify-center min-h-screen bg-gray-50">
      <div className="w-full max-w-md p-8 bg-white rounded-lg shadow-md">
        <h2 className="text-3xl font-bold text-center text-gray-800">
          Welcome back
        </h2>

        {error && <p className="text-red-500 text-center">{error}</p>}

        <form onSubmit={handleSubmit} className="mt-6">
          <div className="mb-4">
            <label htmlFor="phoneNumber" className="block text-gray-700">Phone Number</label>
            <PhoneInput
              international
              defaultCountry="VN"  // You can change the default country as needed
              value={formData.phoneNumber}
              onChange={(value) => setFormData({ ...formData, phoneNumber: value || "" })}
              className="w-full px-4 py-2 mt-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
              placeholder="Enter your phone number"
              disabled={authStep === AuthSteps.InputPassword}
            />
          </div>

          {
            authStep === AuthSteps.InputPassword && (
              <div className="mb-4">
                <label htmlFor="password" className="block text-gray-700">Password</label>
                <input
                  type="password"
                  id="password"
                  name="password"
                  value={formData.password}
                  onChange={handleChange}
                  className="w-full px-4 py-2 mt-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
                  placeholder="Enter your password"
                />
              </div>
            )
          }

          {
            authStep === AuthSteps.InputPhoneNumber && (
              <div className="flex items-center justify-between">
                <Button
                  type="button"
                  onClick={onClickNext}
                  className="w-full mt-4"
                  disabled={loading || formData.phoneNumber.length == 0}
                >
                  {loading ? <Spinner /> : "Continue"}
                </Button>
              </div>
            )
          }

          {
            authStep === AuthSteps.InputPassword && (
              <div className="flex items-center justify-between gap-4">
                <Button
                  type="submit"
                  className="w-full mt-4"
                  onClick={() => setAuthStep(AuthSteps.InputPhoneNumber)}
                  variant={"outline"}
                >
                  Change Phone Number
                </Button>
                <Button
                  type="submit"
                  className="w-full mt-4"
                  disabled={loading || formData.password.length == 0}
                >
                  {loading ? <Spinner /> : "Sign In"}
                </Button>
              </div>
            )
          }

        </form>
      </div>
    </div>
  );
};
