// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("dQM5E1nDU6ruzPC/bxXj74gnTUydhyD1wb+HkvjO0xBZNCG1SsIhiTie/7KiH/93Cql6QjX4bCBtkPKHWcNcotK1K1YFUtSBVLH06+q7oiG0pfApPKpaPjf9R7UpT36MT+n8dKEgTLkQuD2gTRswLiR7U7xVBWTQFKYlBhQpIi0Oomyi0yklJSUhJCdehGeRN0QKcpV0H4J0r0+KLDUDq+VUcS0wZtDXu8Ulea3ekuW+3Oz/+SzYLA3RNkbrafIh3p/Niotjjh4B7PQ8jN2h/tHjrOkU5x0z14m5fWLl6+1M6t99Rt+LhNGX0JXscZDj93mL6Gf7FLD8/2UDTqs6xTADrNumJSskFKYlLiamJSUkpWZQ3mT4nQwiZ+FifdM3SyYnJSQl");
        private static int[] order = new int[] { 12,4,13,10,13,11,12,7,12,11,12,11,12,13,14 };
        private static int key = 36;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
