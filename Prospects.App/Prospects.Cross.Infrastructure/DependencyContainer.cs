using GalaSoft.MvvmLight;
using Prospects.Cross.Infrastructure.Interfaces;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Prospects.Cross.Infrastructure
{
    public static class DependencyContainer 
    {
        public static ILocatorService LocatorService { get; set; } 
    }
}
