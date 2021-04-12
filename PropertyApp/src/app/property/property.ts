export interface Address {
  address1: string;
  address2: string;
  city: string;
  country: string;
  county: string;
  district: string;
  state: string;
  zip: string;
}

export interface Property {
  id: number;
  name: string;
  address: Address;
  yearBuilt: number;
  listPrice: number;
  monthlyRent: number;
  grossYield: number;
  exist: boolean;
}
