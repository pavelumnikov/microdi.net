﻿// MIT License
// -----------
//
// Copyright(c) 2016 Pavel Umnikov
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

using System;
using JetBrains.Annotations;
using microDI.Internal.Assert;
using microDI.Internal.Extensions;

namespace microDI.Internal
{
    internal class AutoWireRegisteredObject : IInternalRegisteredObject
    {
        private readonly IInternalRegisteredObject _registeredObject;

        public Type Type => _registeredObject.Type;
        public ILifeCyclePolicy LifeCycle => _registeredObject.LifeCycle;
        public bool IsInternal { get; private set; }

        public Func<IContainer, object> OverridenInstanceCreatorFunction
            => _registeredObject.OverridenInstanceCreatorFunction;

        public bool HasCustomizedCreatorFunction => _registeredObject.HasCustomizedCreatorFunction;

        public AutoWireRegisteredObject([NotNull] IRegisteredObject registeredObject)
        {
            _registeredObject = RuntimeCheck.NotNull(registeredObject, nameof(registeredObject)).AsInternal();
        }

        public void AsInternalInjection()
        {
            IsInternal = true;
        }
    }
}
