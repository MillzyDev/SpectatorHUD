#include <iomanip>
#include <sstream>

extern "C" {
    // i want SH to have a minimal impact on performance. formatting a float to a string is quite slow in C#, hence this
    _declspec(dllexport) auto format_float(float value, const int decimal_precision, const char** ret) -> void  // NOLINT(clang-diagnostic-language-extension-token)
    {
        std::stringstream ss;
        ss << std::fixed << std::setprecision(decimal_precision);
        const char* str = ss.str().c_str();  // NOLINT(clang-diagnostic-dangling-gsl)
        *ret = str;
    }
}

