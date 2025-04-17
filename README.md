# 🧪 QA Test Suite: Katalon Automated Recorder – Web App Permissions Project

**Author**: Michael Blair  
**Framework**: [Katalon Recorder (Chrome Extension)](https://chromewebstore.google.com/detail/katalon-recorder-selenium/ljdobmomdgdljniojadhoplhkpialdid)  
**Web Driver**: https://storage.googleapis.com/chrome-for-testing-public/135.0.7049.95/win64/chromedriver-win64.zip
**Project Status**: Archived (test site deprecated)

---

## 📋 Project Overview

This project involved the creation of three structured automated test suites (Admin, User, and Cleanup) for a permission-based user database hosted on the Mohawk College CSUNIX server. All test cases were created using the **Katalon Recorder** browser extension and were designed to validate:

- User and Admin permission boundaries (User Authorization)
- Username and password validation rules (Input Validation)
- Site navigation, search, and data manipulation
- Edge cases around account creation and deletion

### 🔍 Project Scope:
- 43 Total Test Cases
- 3 Test Suites
- 2 Discovered Bugs (documented and triaged)
- Includes bug documentation, expected vs. actual outcomes, and full test procedure guide

---

## 🧪 Test Suites Breakdown

### `1_ADMIN_TEST.krecorder`
- Covers admin account creation, permissions toggling, user management, and debug settings

### `2_USER_TEST.krecorder`
- Covers non-admin account creation, login, navigation through member-specific features (news, search, FAQ)

### `3_CLEANUP_TEST.krecorder`
- Deletes all test data created in the prior suites, verifying that accounts were properly removed


Each bug includes supporting test case IDs, reproduction steps, and suggestions for resolution.. See full write-up in [ResultDocumentation.docx](./ResultDocumentation.docx).

---

## 🧑‍🔬 How It Was Tested

- **Test Environment**: Chrome browser on 1920x1080 resolution, Katalon Recorder extension installed
- **Execution Method**: Test cases loaded as `.krecorder` suites and run via Recorder’s built-in player
- **Expected Outputs**: Verified through on-screen text, system messages, and conditional logic assertions
- **Files Included**:
  - `.krecorder` files and how to use them.
  - Full bug and test documentation
  - Visual guide and video demo (offline/local)

---

## 🔒 Why the Tests Are Not Currently Executable

The target web application was hosted on the college’s internal server (`csunix.mohawkcollege.ca`) and has since been decommissioned/moved. The documentation and test case logic are preserved here for experiential, educational, and demonstrational purposes as a reference.

---

## 🗂 File Structure

```plaintext
SeleniumAssignment/
├── 1_ADMIN_TEST.krecorder         # Katalon test: Admin specific functionality
├── 2_USER_TEST.krecorder          # Katalon test: Standard user actions
├── 3_CLEANUP_TEST.krecorder       # Katalon test: Cleanup process
├── Katalon_TestSuites.zip         # Zipped test suite collection
├── ResultDocumentation.docx       # Bug summary and test case write-up
└── TestSuiteExecution_Demo.mp4    # Demo video of suite execution
```

## 🧠 Reflections

This project helped develop skills in:
- UI automation
- Boundary testing
- Structured test planning
- Bug reproduction and analysis
- Communicating defects with clarity and precision

---

## 📄 License

All test artifacts are original academic work under fair use for portfolio display.
