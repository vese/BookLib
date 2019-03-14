using System;

namespace CommonHelpers {

[AttributeUsage(
   AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Constructor |
   AttributeTargets.Class | AttributeTargets.Struct )]
public class HThreadSafeAttribute: Attribute {}

[AttributeUsage(
   AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Constructor |
   AttributeTargets.Class | AttributeTargets.Struct )]
public class HThreadStaticAttribute: Attribute {}

[AttributeUsage(AttributeTargets.Method|AttributeTargets.Property)]
public class HSynchronizedObjectAttribute: Attribute {}

[AttributeUsage(
   AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Constructor |
   AttributeTargets.Class | AttributeTargets.Struct )]
public class HLockOnStaticSyncRootAttribute: Attribute {}

[AttributeUsage(
   AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Constructor |
   AttributeTargets.Class | AttributeTargets.Struct )]
public class HLockOnCurrentAttribute: Attribute {}

[AttributeUsage(AttributeTargets.Method|AttributeTargets.Property|AttributeTargets.Constructor)]
public class HNeedsInLockAttribute: Attribute {}

[AttributeUsage(AttributeTargets.Method|AttributeTargets.Property|AttributeTargets.Constructor)]
public class HMainThreadOnlyAttribute: Attribute {}

[AttributeUsage(AttributeTargets.Method|AttributeTargets.Property|AttributeTargets.Constructor)]
public class HNotInMainThreadAttribute: Attribute {}

[AttributeUsage(AttributeTargets.Method|AttributeTargets.Property|AttributeTargets.Constructor)]
public class HMarshalingToMainUIThreadAttribute: Attribute {}

[AttributeUsage(AttributeTargets.Property)]
public class HSetInMainThreadOnlyAttribute: Attribute {}

[AttributeUsage(AttributeTargets.Method|AttributeTargets.Property|AttributeTargets.Constructor)]
public class HLockFreeAttribute: Attribute {}

[AttributeUsage(AttributeTargets.Method|AttributeTargets.Property)]
public class HUnlockedAttribute: Attribute {}

[AttributeUsage(AttributeTargets.Method|AttributeTargets.Property|AttributeTargets.Constructor)]
public class HRequiresLockAttribute: Attribute {}

[AttributeUsage(AttributeTargets.Parameter|AttributeTargets.Property|AttributeTargets.Class)]
public class HVolatileAccessAttribute: Attribute {}

[AttributeUsage(AttributeTargets.Method|AttributeTargets.Property|AttributeTargets.Constructor)]
public class HMemoryBarrierAttribute: Attribute {}

[AttributeUsage(
   AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Constructor |
   AttributeTargets.Parameter )]
public class HLockAttribute: Attribute {}

[AttributeUsage(
   AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Constructor |
   AttributeTargets.Parameter )]
public class HHasLocksAttribute: Attribute {}

[AttributeUsage(AttributeTargets.Parameter)]
public class HPartialLockAttribute: Attribute {}

} // CommonHelpers
