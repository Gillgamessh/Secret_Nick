export function formatPhoneNumber(phoneNumber?: string): string {
  const digits = phoneNumber?.replace(/[^\d+]/g, "");

  const match = digits?.match(/^(\+38)(0\d{2})(\d{3})(\d{2})(\d{2})$/);
  if (!match) {
    return phoneNumber ?? "";
  }

  const [, country, operator, part1, part2, part3] = match;
  return `${country} ${operator} ${part1} ${part2} ${part3}`;
}
