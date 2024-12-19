export interface User {
  id: number;
  guid: string;
  email: string;
  name: string;
  authMethod: string;
  authMethodId: string;
  createdAt: Date;
}
