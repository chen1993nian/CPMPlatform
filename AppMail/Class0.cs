using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;

internal class Class0 : ICertificatePolicy
{
	public Class0()
	{
	}

	public bool CheckValidationResult(ServicePoint service_point, X509Certificate cert, WebRequest web_request, int certificate_problem)
	{
		return true;
	}
}