import { User } from './user.model';

export interface Url {
  id?: number;
  guid?: string;
  baseUrl: string;
  alias: string;
  description?: string;
  userId?: number;
  user?: User;
  visitCount?: number;
  createdAt?: Date;
}
