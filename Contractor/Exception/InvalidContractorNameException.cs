using BuildingBlocks.Exception;

namespace Contractor.Exception;

public class InvalidContractorNameException() : LogicException("Name Is Required And Must Grater Then 2 Character");