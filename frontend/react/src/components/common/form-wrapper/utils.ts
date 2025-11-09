import { FORM_CONTENT_MAP, ICON_NAMES } from "./types";

export function getFormContent(formKey: keyof typeof FORM_CONTENT_MAP) {
  return FORM_CONTENT_MAP[formKey];
}

export function getDescriptionWrapperClass(iconName: string): string {
  const classMap: Record<string, string> = {
    [ICON_NAMES.WELCOME_GROUP]:
      "form-wrapper__description-wrapper--welcome-group",
    [ICON_NAMES.WREATH]: "form-wrapper__description-wrapper--wreath",
    [ICON_NAMES.STAR]: "form-wrapper__description-wrapper--star",
  };

  return classMap[iconName] || "";
}
