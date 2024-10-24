using BuildingBlocks.Exception;

namespace Tender.Exception;

public class TenderInvalidStateForCloseException() : LogicException("This Tender Not Have a Bid");
