# microDI.NET #

Tiny dependency injection container for small applications. 

## Current build Status

[![Build status](https://ci.appveyor.com/api/projects/status/mpm8grel7w9gng3w?retina=true)](https://ci.appveyor.com/project/pavel-xrayz13/microdi-net-0spbd)

### What? Another IoC container? ###

Yes, this is another IoC container. It facilitates building small and tiny applications and provides developers with the following advantages:

* Small library without additional dependencies.
* Simplified object creation. Programmer can choose how to create: automatically or customized.
* No special namings or other things to register multiple implementations of same interface: one interface, one implementation.
* Custom life cycle policy defintion.

### How do I get set up? ###

Minimalist setup is to create instance of Container class and that's all.

Basic usage is that:
```cs
// IoC container setup
var container = new Container();

// Register MyClass as transient: on every resolve activator factory will produce new instance
container.Register<MyClass>(new TransientLifeCyclePolicy());

// Resolve instance somewhere
var instance = container.Resolve<MyClass>();
```

There are three life cycle policies to be used:

* Transient policy
* Singleton policy
* Thread-local singleton policy

Usages of these life cycle policies:
```cs
// Singleton life cycle policy example
container.Register<MyClass>(new SingletonLifeCyclePolicy());

// Thread-local singleton life cycle policy example
container.Register<MyOtherClass>(new ThreadLocalLifeCyclePolicy());
```

Interface and implementation registering:
```cs
// Register interface
container.RegisterAs<IMyInterface, MyInterfaceImplementation>(new TransientLifeCyclePolicy());

// Resolve it somewhere
var result = container.Resolve<IMyInterface>();
```

All examples above are using automatic injection of dependency of dependencies it's default behaviour. MicroDI has feature to do customized instance creation via user-provided delegate:
```cs
// Register class via custom initializer
container.Register<MyClass>(
    c => new MyClass(c.Resolve<Dependency1>(), c.Resolve<Dependency2>()),
    new TransientLifeCyclePolicy());

// Register interface via custom initializer
container.RegisterAs<IMyInterface, MyInterfaceImplementation>(
    c => new MyInterfaceImplementation(c.Resolve<Dependency1>(), c.Resolve<Dependency2>()),
    new TransientLifeCyclePolicy());
```

Also there is an auto-wiring addition that could help in situations, where you have implementation with N-interfaces. Such one of interfaces is also wanted to be resolved, but using it's already registered implementation without duplicating information inside container. Here is small example of such feature:
```cs
// IFirstInterface.cs
interface IFirstInterface {};

// ISecondInterface.cs
interface ISecondInterface {};

// ImplementorOfTwoInterfaces.cs
class ImplementorOfTwoInterfaces: IFirstInterface, ISecondInterface {}

// Registering implementor in your entry point
container.RegisterAs<IFirstInterface, ImplementorOfTwoInterfaces>(new TransientLifeCyclePolicy()).AutoWire<ISecondInterface>();

// Resolve them elsewhere

// Primary resolve, by using information of registered type
var first = container.Resolve<IFirstInterface>();

// Secondary resolve by referencing auto-wired type to primary.
var second = container.Resolve<ISecondInterface>();
```

Also, you can set auto-wire lately, by accessing type-object information that is stored in container's registry service:
```cs
// All interfaces and class are same as in previous auto-wire example

// Registering implementor in your entry point
container.RegisterAs<IFirstInterface, ImplementorOfTwoInterfaces>(new TransientLifeCyclePolicy());

// Lately call GetReferencedType method to access publically available internal state of type-object and auto-wire with ISecondInterface
container.GetReferencedType<IFirstInterface>().AutoWire<ISecondInterface>();
```

Not only interface auto-wiring is a special feature. There is another option to use interfaces: make them only accessible by the IoC container when resolving. When you try to resolve such interface manually, you'll get ActivationException with inner OnlyForInternalUseException type of exception. Simple use case:
```cs
// IFirstInterface.cs
interface IFirstInterface {};

// Registering implementor in your entry point and make it usable only from IoC internal calls
container.RegisterAs<IFirstInterface, ImplementorOfTwoInterfaces>(new TransientLifeCyclePolicy()).ForInternalUse();
```

More complex use case example:
```cs
// IFirstInterface.cs
interface IFirstInterface {};

// ISecondInterface.cs
interface ISecondInterface {};

// ImplementorOfFirstInterface.cs
class ImplementorOfFirstInterface: IFirstInterface {}

// ImplementorOfSecondInterface.cs
class ImplementorOfSecondInterface : ISecondInterface
{
		public ImplementorOfSecondInterface(IFirstInterface firstInterface)
        {}
}

// Registering first implementor in your entry point
container.RegisterAs<IFirstInterface, ImplementorOfFirstInterface>(new TransientLifeCyclePolicy()).ForInternalUse();

// Registering second implementor in your entry point
container.RegisterAs<ISecondInterface, ImplementorOfSecondInterface>(new TransientLifeCyclePolicy()).ForInternalUse();

// Resolve second interface elsewhere

// Second interface will be resolved by injecting new instance of IFirstInterface implementation in consructor
var secondInterface = container.Resolve<ISecondInterface>();
``` 

Customizations of life cycle policies is another feature. All implementations must be inherited from *ILifeCyclePolicy* interface. Here is small example of implementation singleton that is used in the library:
```cs
class SingletonLifeCyclePolicy : ILifeCyclePolicy
{
    private object _value;

    public object Get(IRegistryAccessorService registryAccessorService, IActivationService activationService, Type type)
    {
        object result;

        if (_value != null)
            result = _value;
        else
            result = _value = activationService.GetInstance(registryAccessorService.GetRegisteredObject(type));

        return result;
    }
}
```