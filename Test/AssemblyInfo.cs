<<<<<<< HEAD
﻿using Xunit.Sdk;
using Xunit.v3;

[assembly: Parallelization(Mode = ParallelMode.None)]
=======
﻿using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

>>>>>>> a36128de87f15375dd7813da847dbd6258f457fd
