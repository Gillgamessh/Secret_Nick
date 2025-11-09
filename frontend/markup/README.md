# Project Setup and Component Navigation

## Steps to Add and View a New Component

1. **Install Project Dependencies**
   - In your terminal, run:
  
     ```bash
     npm install
     ```

2. **Create the Component Folder**
   - Inside the `components` directory, create a new folder named after your component.
   - Inside this new folder, create two files:
     - `<component-name>.html`
     - `<component-name>.scss`

3. **Add a Link to the Component**
   - Open the `src/index.html` file.
   - Create an `<a>` tag with:
     - `href` attribute pointing to the path of the component's `.html` file.
     - The link text should be the component's name.

   **Example:**
  
   ```html
   <a href="components/MyComponent/MyComponent.html">MyComponent</a>
   ```

4. **Run the Project**
   - In your terminal, run:

    ```bash
     npm run dev
     ```

5. **Navigate to Your Component**
    - Open the running project in your browser.
    - Click the link you created in index.html to navigate to your component's page.
