using System;

namespace CommonHelpers {

[AttributeUsage(AttributeTargets.Parameter)]
public class HInAttribute: Attribute {}

[AttributeUsage(AttributeTargets.Parameter)]
public class HOutAttribute: Attribute {}

[AttributeUsage(AttributeTargets.Parameter)]
public class HOptionalAttribute: Attribute {}

[AttributeUsage(AttributeTargets.Property)]
public class HNotNullAttribute: Attribute {}

[AttributeUsage(AttributeTargets.Parameter)]
public class HPartiallyOptionalAttribute: Attribute {}

[AttributeUsage(AttributeTargets.Property)]
public class HOptionalValueAttribute: Attribute {}

[AttributeUsage(AttributeTargets.Method)]
public class HOptionalResultAttribute: Attribute {}

[AttributeUsage(AttributeTargets.Method)]
public class HReturnsFalseAttribute: Attribute {}

[AttributeUsage(AttributeTargets.Method)]
public class HReturnsTrueAttribute: Attribute {}

[AttributeUsage(AttributeTargets.Method)]
public class HReturnsCurrentAttribute: Attribute {}

[AttributeUsage(AttributeTargets.Method)]
public class HReturnsPreviousAttribute: Attribute {}

[AttributeUsage(AttributeTargets.Method|AttributeTargets.Property)]
public class HStrictResultAttribute: Attribute {}

[AttributeUsage(AttributeTargets.Property)]
public class HForReadAttribute: Attribute {}

[AttributeUsage(AttributeTargets.Method|AttributeTargets.Property)]
public class HReadonlyObjectAttribute: Attribute {}

[AttributeUsage(AttributeTargets.Method|AttributeTargets.Property|AttributeTargets.Parameter)]
public class HZeroBasedIndexAttribute: Attribute {}

[AttributeUsage(AttributeTargets.Method|AttributeTargets.Property|AttributeTargets.Parameter)]
public class HOneBasedIndexAttribute: Attribute {}

[AttributeUsage(AttributeTargets.Class)]
public sealed class HSyncOnlyDisposableAttribute: Attribute {}

} // CommonHelpers
