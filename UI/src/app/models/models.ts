export interface User {
  id: number;
  name: string;
  email: string;
  mobileNumber: string;
  password: string;
  userType: UserType;
  accountStatus: AccountStatus;
  createdOn: string;
  tokensAvailable:number;
}

export enum AccountStatus {
  UNAPROOVED,
  ACTIVE,
  BLOCKED,
}

export enum UserType {
  ADMIN,
  STUDENT,
}

export interface BookCategory {
  id: number;
  category: string;
  subCategory: string;
}

export interface Book {
  id: number;
  bookName: string;
  author: string;
  price: number;
  ordered: boolean;
  userId:number;
  bookCategoryId: number;
  bookCategory: BookCategory;
  description:string;
  rating:number;
}

export interface BooksByCategory {
  bookCategoryId: number;
  category: string;
  subCategory: string;
  books: Book[];
}

export interface Order {
  id: number;
  userId: number;
  userName: string | null;
  bookId: number;
  bookName: string;
  borrowDate: string;
  returned: boolean;
  returnDate: string | null;
  finePaid: number;
}
