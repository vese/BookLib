using System;

namespace CommonHelpers {

//**************************************************************************************************
public class HDeadCodeBranchException: HException {
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

public HDeadCodeBranchException()
   :base( "Codeflow entered in a some dead branch." )
{}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
} // HDeadCodeBranchException
//**************************************************************************************************

} // CommonHelpers
