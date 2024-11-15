# SwagLabsTestTask: Automated Testing

## Task Description

This task involves creating automated tests to verify the functionality of the login form on the [Swag Labs](https://www.saucedemo.com/) website. The task includes implementing and testing three main use cases (UCs).

---

## Use Cases (UC)

### **UC-1: Testing the login form with empty fields**
1. Enter any data into the **"Username"** and **"Password"** fields.
2. Clear the input values.
3. Click the **"Login"** button.
4. Verify the error message: **"Username is required"**.

### **UC-2: Testing the login form with only the username provided**
1. Enter any value into the **"Username"** field.
2. Enter a password into the **"Password"** field.
3. Clear the **"Password"** field.
4. Click the **"Login"** button.
5. Verify the error message: **"Password is required"**.

### **UC-3: Testing the login form with valid credentials**
1. Enter a username listed under the **Accepted usernames** section.
2. Enter the password: **"secret_sauce"**.
3. Click the **"Login"** button.
4. Verify that the page title displays as **"Swag Labs"**.

---

## Implementation Requirements

### Key Conditions
- Support **parallel execution** of tests.
- Add **logging** for test results.
- Use a **Data Provider** to parameterize the tests.

### Tools and Technologies
- **Test Automation Tool**: Selenium WebDriver.
- **Browsers**:
  1. Firefox;
  2. Chrome.
- **Locators**: XPath.
- **Test Runner**: xUnit.
- **Logging**: SeriLog (optional).
- **Design Patterns** (optional):
  - Singleton;
  - Adapter;
  - Strategy.
- **Test Automation Approach** (optional): BDD.
- **Assertion Library**: Fluent Assertions.

---

## Setup Instructions

1. Clone the repository.
2. Configure the environment:
   - Install the required browsers (Chrome and Firefox).
   - Install dependencies for Selenium and xUnit.
3. Run tests via the test runner or command line.

---

## Task Objective

The goal of this task is to implement reliable, scalable, and maintainable test automation to ensure the quality of the Swag Labs web application. The implementation should follow best practices, including parallel test execution, parameterization, and logging.
