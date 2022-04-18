import { UserPreferences } from "./user-preferences";

export interface User {
    username: string;
    email: string;
    userPreferences: UserPreferences;
}