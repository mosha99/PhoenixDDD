using BuildingBlocks.Exception;

namespace Contractor.Exception;

public class InvalidContractorPhoneNumberException() : LogicException("PhoneNumber Is Required And Must Then 11 Character");