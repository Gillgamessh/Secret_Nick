import { PersonalInfoTerm } from '../app.enum';

const personalInfoConfig = [
  { term: PersonalInfoTerm.FirstName, key: 'firstName' },
  { term: PersonalInfoTerm.LastName, key: 'lastName' },
  { term: PersonalInfoTerm.Phone, key: 'phone' },
  { term: PersonalInfoTerm.Email, key: 'email' },
  { term: PersonalInfoTerm.DeliveryInfo, key: 'deliveryInfo' },
] as const;

export const getPersonalInfo = (
  user: Partial<Record<(typeof personalInfoConfig)[number]['key'], string>> = {}
) => {
  return personalInfoConfig.map(({ term, key }) => ({
    term,
    value: user[key] || '',
  }));
};
