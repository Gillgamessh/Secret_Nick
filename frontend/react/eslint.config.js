import js from "@eslint/js";
import globals from "globals";
import reactPlugin from "eslint-plugin-react";
import reactHooks from "eslint-plugin-react-hooks";
import reactRefresh from "eslint-plugin-react-refresh";
import prettierPlugin from "eslint-plugin-prettier";
import configPrettier from "eslint-config-prettier";
import typescriptPlugin from "@typescript-eslint/eslint-plugin";
import typescriptParser from "@typescript-eslint/parser";
import { defineConfig, globalIgnores } from "eslint/config";

export default defineConfig([
  globalIgnores(["dist"]),
  {
    files: ["**/*.{js,jsx,ts,tsx}"],
    extends: [
      js.configs.recommended,
      reactPlugin.configs.flat.recommended,
      reactHooks.configs["recommended-latest"],
      reactRefresh.configs.vite,
    ],
    plugins: {
      react: reactPlugin,
      prettier: prettierPlugin,
      "@typescript-eslint": typescriptPlugin,
    },
    languageOptions: {
      ecmaVersion: "latest",
      globals: {
        ...globals.browser,
        RequestInit: true,
      },
      parser: typescriptParser,
      parserOptions: {
        ecmaFeatures: { jsx: true },
        sourceType: "module",
      },
    },
    rules: {
      // prettier rules
      ...prettierPlugin.configs.recommended.rules, // use prettier-recommended rules
      ...configPrettier.rules, // disable conflicting ESLint rules with Prettier
      "prettier/prettier": [
        "error",
        {
          endOfLine: "auto",
        },
      ],

      // typescript rules
      ...typescriptPlugin.configs.recommended.rules,

      // react rules
      "react/react-in-jsx-scope": "off",
      "react/jsx-uses-react": "off",
      "react/jsx-boolean-value": "error",
      "react/self-closing-comp": "error",
      "react/jsx-curly-brace-presence": ["error", "never"],
      "react/no-unknown-property": "error",

      // general rules
      "linebreak-style": "off",
      "no-console": ["error", { allow: ["warn", "error"] }],
    },
    settings: {
      react: {
        version: "detect", // automatically detect the React version
      },
    },
  },
]);
