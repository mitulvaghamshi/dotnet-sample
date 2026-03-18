# CS Overview

In 2000, Microsoft introduced the C# programming language, which draws its inspiration from the C, C++, and Java programming languages.

### Index

| No  | Topic                                                       |
| :-: | :---------------------------------------------------------- |
|  1  | [Introduction to C#](#introduction-to-c)                    |
|  2  | [Object-Oriented Programming](#object-oriented-programming) |
|  3  | [Event-Driven Programming](#event-driven-programming)       |
|  4  | [Visual Programming](#visual-programming)                   |
|  5  | [Generic Programming](#generic-programming)                 |
|  6  | [Functional Programming](#functional-programming)           |
|  7  | [Microsoft's .NET](#microsofts-net)                         |
|  8  | [.NET Framework](#net-framework)                            |
|  9  | [Common Language Runtime](#common-language-runtime)         |
| 10  | [Language Interoperability](#language-interoperability)     |

## Object-Oriented Programming

C# is an object-oriented programming language. It has access to the powerful .NET Framework Class Library, a vast collection of prebuilt classes that facilitate rapid app development. The .NET Framework enables C# to seamlessly interact with Windows, allowing programs to leverage familiar Windows features.

Key capabilities of the .NET Framework Class Library include:

|                   |                 |                     |                                |
| :---------------- | :-------------- | :------------------ | :----------------------------- |
| Database          | Debugging       | Computer networking | Web communication              |
| Building web apps | Multithreading  | Permissions         | Graphical user interface       |
| Graphics          | File processing | Mobile              | Data structures                |
| Input/output      | Security        | String processing   | Universal Windows Platform GUI |

## Event-Driven Programming

C# Graphical user interfaces (GUIs) are event-driven, allowing you to create programs that respond to user-initiated events like mouse clicks, keystrokes, timer expirations, and gestures such as touches and finger swipes, which are commonly used on smartphones and tablets.

## Visual Programming

Microsoft's Visual Studio is a visual programming language that allows you to use C#. With Visual Studio, you can easily drag and drop predefined GUI objects like buttons and textboxes onto your screen, label and resize them, and Visual Studio will generate much of the corresponding GUI code.

## Generic Programming

It's common to write a program that processes a collection of items. Historically, you had to write separate code to handle each type of collection. With generic programming, you write code that handles a collection "in the general" and C# handles the specifics for each different type of collection.

## Functional Programming

Functional programming allows you to specify the desired outcome of a task without detailing the specific steps required to achieve it. For instance, with Microsoft's LINQ, you can simply request the sum of a collection of numbers without specifying the mechanics of traversing the elements and accumulating the total. LINQ takes care of all the necessary operations. This approach not only accelerates application development but also minimizes errors.

## Microsoft's .NET

In 2000, Microsoft unveiled its .NET initiative (www.microsoft.com/net), a comprehensive vision for leveraging the Internet and the web in software development, engineering, distribution, and usage. Unlike forcing users to adhere to a single programming language, .NET empowers developers to create applications in any .NET-compatible language, including C#, Visual Basic, Visual C++, and others.

## .NET Framework

The .NET Framework Class Library offers numerous capabilities that facilitate the rapid and effortless creation of substantial C# applications. It comprises thousands of valuable prebuilt classes that have undergone rigorous testing and optimization to ensure optimal performance. Whenever feasible, it is advisable to re-utilize the .NET Framework classes to expedite the software development process, thereby enhancing the quality and performance of the software you develop.

## Common Language Runtime

The Common Language Runtime (CLR) is a virtual machine (VM) that executes .NET programs and provides functionalities to simplify their development and debugging. The source code for programs managed by the CLR is referred to as managed code. The CLR offers various services to manage code, including integrating software components written in different .NET languages, error handling between components, enhanced security, automatic memory management, and more. On the other hand, unmanaged-code programs lack access to the CLR's services, making them more challenging to write.

Managed code undergoes compilation into machine-specific instructions through the following steps:

- First, the code is compiled into Microsoft Intermediate Language (MSIL).
- The CLR can weave together code converted from other languages and sources into MSIL, allowing programmers to work in their preferred .NET programming language.
- The MSIL for an app's components is placed into the app's executable file, which triggers the computer to perform the app's tasks.
- When the app executes, the CLR's just-in-time compiler (JIT compiler) translates the MSIL in the executable file into machine-language code specific to the platform.
- The machine-language code executes on the chosen platform.

## Language Interoperability

The .NET Framework offers high-level language interoperability. Since software components written in different .NET languages (like C# and Visual Basic) are all compiled into MSIL, they can be combined to create a single, unified program. This makes the .NET Framework language-independent.
