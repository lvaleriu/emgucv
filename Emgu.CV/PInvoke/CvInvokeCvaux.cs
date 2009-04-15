using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Emgu.CV.Structure;

namespace Emgu.CV
{
   public partial class CvInvoke
   {
      /*
        /// <summary>
        /// Wrapped CvCallBack function pointer in CvAux
        /// </summary>
        /// <param name="index">index for the elements of user data</param>
        /// <param name="buffer">buffer used to store the returned element</param>
        /// <param name="user_data">user data</param>
        /// <returns>error code</returns>
        public delegate int CvCallBack(int index, IntPtr buffer, ref MUserData user_data);
        */

      #region Eigen Objects
      #region cvEigenDecomposite function
      /// <summary>
      /// Calculates all decomposition coefficients for the input object using the previously calculated eigen objects basis and the averaged object
      /// </summary>
      /// <param name="obj">Input object (Pointer to IplImage)</param>
      /// <param name="eigInput">Pointer to the array of IplImage input objects</param>
      /// <param name="avg">Averaged object (Pointer to IplImage)</param>
      /// <return>Calculated coefficients; an output parameter</return>
      public static float[] cvEigenDecomposite(
          IntPtr obj,
          IntPtr[] eigInput,
          IntPtr avg)
      {
         float[] coeffs = new float[eigInput.Length];
         cvEigenDecomposite(
             obj,
             eigInput.Length,
             eigInput,
             CvEnum.EIGOBJ_TYPE.CV_EIGOBJ_NO_CALLBACK,
             IntPtr.Zero,
             avg,
             coeffs);
         return coeffs;
      }

      /// <summary>
      /// Calculates all decomposition coefficients for the input object using the previously calculated eigen objects basis and the averaged object
      /// </summary>
      /// <param name="obj">Input object (Pointer to IplImage)</param>
      /// <param name="eigenvecCount">Number of eigen objects</param>
      /// <param name="eigInput">Pointer either to the array of IplImage input objects or to the read callback function according to the value of the parameter <paramref name="ioFlags"/></param>
      /// <param name="ioFlags">Input/output flags</param>
      /// <param name="userData">Pointer to the structure that contains all necessary data for the callback functions</param>
      /// <param name="avg">Averaged object (Pointer to IplImage)</param>
      /// <param name="coeffs">Calculated coefficients; an output parameter</param>
      [DllImport(CVAUX_LIBRARY)]
      private static extern void cvEigenDecomposite(
         IntPtr obj,
         int eigenvecCount,
         IntPtr[] eigInput,
         CvEnum.EIGOBJ_TYPE ioFlags,
         IntPtr userData,
         IntPtr avg,
         float[] coeffs);

      #endregion

      /// <summary>
      /// Calculates orthonormal eigen basis and the averaged object for a group of the input objects.
      /// </summary>
      /// <param name="input">Pointer to the array of IplImage input objects </param>
      /// <param name="calcLimit">Criteria that determine when to stop calculation of eigen objects. Depending on the parameter calcLimit, calculations are finished either after first calcLimit.max_iter dominating eigen objects are retrieved or if the ratio of the current eigenvalue to the largest eigenvalue comes down to calcLimit.epsilon threshold. The value calcLimit -> type must be CV_TERMCRIT_NUMB, CV_TERMCRIT_EPS, or CV_TERMCRIT_NUMB | CV_TERMCRIT_EPS . The function returns the real values calcLimit->max_iter and calcLimit->epsilon</param>
      /// <param name="avg">Averaged object</param>
      /// <param name="eigVals">Pointer to the eigenvalues array in the descending order; may be NULL</param>
      /// <param name="eigVecs">Pointer either to the array of eigen objects</param>
      /// <return>Pointer either to the array of eigen objects or to the write callback function</return>
      public static void cvCalcEigenObjects(
         IntPtr[] input,
         ref MCvTermCriteria calcLimit,
         IntPtr[] eigVecs,
         float[] eigVals,
         IntPtr avg)
      {
         cvCalcEigenObjects(
             input.Length,
             input,
             eigVecs,
             CvEnum.EIGOBJ_TYPE.CV_EIGOBJ_NO_CALLBACK,
             0,
             IntPtr.Zero,
             ref calcLimit,
             avg,
             eigVals);
      }

      /// <summary>
      /// Calculates orthonormal eigen basis and the averaged object for a group of the input objects.
      /// </summary>
      /// <param name="nObjects">Number of source objects</param>
      /// <param name="input">Pointer either to the array of IplImage input objects or to the read callback function</param>
      /// <param name="output">Pointer either to the array of eigen objects or to the write callback function</param>
      /// <param name="ioFlags">Input/output flags</param>
      /// <param name="ioBufSize">Input/output buffer size in bytes. The size is zero, if unknown</param>
      /// <param name="userData">Pointer to the structure that contains all necessary data for the callback functions</param>
      /// <param name="calcLimit">Criteria that determine when to stop calculation of eigen objects. Depending on the parameter calcLimit, calculations are finished either after first calcLimit.max_iter dominating eigen objects are retrieved or if the ratio of the current eigenvalue to the largest eigenvalue comes down to calcLimit.epsilon threshold. The value calcLimit -> type must be CV_TERMCRIT_NUMB, CV_TERMCRIT_EPS, or CV_TERMCRIT_NUMB | CV_TERMCRIT_EPS . The function returns the real values calcLimit->max_iter and calcLimit->epsilon</param>
      /// <param name="avg">Averaged object</param>
      /// <param name="eigVals">Pointer to the eigenvalues array in the descending order; may be NULL</param>
      [DllImport(CVAUX_LIBRARY)]
      private static extern void cvCalcEigenObjects(
         int nObjects,
         IntPtr[] input,
         IntPtr[] output,
         CvEnum.EIGOBJ_TYPE ioFlags,
         int ioBufSize,
         IntPtr userData,
         ref MCvTermCriteria calcLimit,
         IntPtr avg,
         float[] eigVals);

      /// <summary>
      /// Calculates an object projection to the eigen sub-space or, in other words, restores an object using previously calculated eigen objects basis, averaged object, and decomposition coefficients of the restored object. 
      /// </summary>
      /// <param name="inputVecs">Pointer to either an array of IplImage input objects or to a callback function, depending on io_flags</param>
      /// <param name="coeffs">Previously calculated decomposition coefficients</param>
      /// <param name="avg">Average vector</param>
      /// <param name="proj">Projection to the eigen sub-space</param>
      public static void cvEigenProjection(
         IntPtr[] inputVecs,
         float[] coeffs,
         IntPtr avg,
         IntPtr proj)
      {
         CvInvoke.cvEigenProjection(
             inputVecs,
             inputVecs.Length,
             CvEnum.EIGOBJ_TYPE.CV_EIGOBJ_NO_CALLBACK,
             IntPtr.Zero,
             coeffs,
             avg,
             proj);
      }

      /// <summary>
      /// Calculates an object projection to the eigen sub-space or, in other words, restores an object using previously calculated eigen objects basis, averaged object, and decomposition coefficients of the restored object. Depending on io_flags parameter it may be used either in direct access or callback mode.
      /// </summary>
      /// <param name="inputVecs">Pointer to either an array of IplImage input objects or to a callback function, depending on io_flags</param>
      /// <param name="eigenvecCount">Number of eigenvectors</param>
      /// <param name="ioFlags">Input/output flags</param>
      /// <param name="userdata">Pointer to the structure that contains all necessary data for the callback functions</param>
      /// <param name="coeffs">Previously calculated decomposition coefficients</param>
      /// <param name="avg">Average vector</param>
      /// <param name="proj">Projection to the eigen sub-space</param>
      [DllImport(CVAUX_LIBRARY)]
      private extern static void cvEigenProjection(
         IntPtr[] inputVecs,
         int eigenvecCount,
         CvEnum.EIGOBJ_TYPE ioFlags,
         IntPtr userdata,
         float[] coeffs,
         IntPtr avg,
         IntPtr proj);
      #endregion

      #region background / forground  statistic
      /// <summary>
      /// Create a Gaussian background model
      /// </summary>
      /// <param name="image">Background image</param>
      /// <param name="param">Parameters for the background model</param>
      [DllImport(CVAUX_LIBRARY)]
      public extern static IntPtr cvCreateGaussianBGModel(IntPtr image, IntPtr param);

      /// <summary>
      /// Create a background model
      /// </summary>
      /// <param name="image">Background image</param>
      /// <param name="param">Parameters for the background model</param>
      [DllImport(CVAUX_LIBRARY)]
      public extern static IntPtr cvCreateFGDStatModel(IntPtr image, IntPtr param);

      /// <summary>
      /// Create a forground model
      /// </summary>
      /// <param name="firstFrame">The first frame</param>
      /// <param name="parameters">The forground statistic parameters</param>
      /// <returns>Pointer to the forground model</returns>
      [DllImport(CVAUX_LIBRARY)]
      public extern static IntPtr cvCreateFGDStatModel(IntPtr firstFrame, ref MCvFGDStatModelParams parameters);
      #endregion

      /// <summary>
      /// Calculates disparity for stereo-pair 
      /// </summary>
      /// <param name="leftImage">Left image of stereo pair, rectified grayscale 8-bit image</param>
      /// <param name="rightImage">Right image of stereo pair, rectified grayscale 8-bit image</param>
      /// <param name="mode">Algorithm used to find a disparity</param>
      /// <param name="depthImage">Destination depth image, grayscale 8-bit image that codes the scaled disparity, so that the zero disparity (corresponding to the points that are very far from the cameras) maps to 0, maximum disparity maps to 255.</param>
      /// <param name="maxDisparity">Maximum possible disparity. The closer the objects to the cameras, the larger value should be specified here. Too big values slow down the process significantly</param>
      /// <param name="param1">constant occlusion penalty</param>
      /// <param name="param2">constant match reward</param>
      /// <param name="param3">defines a highly reliable region (set of contiguous pixels whose reliability is at least param3)</param>
      /// <param name="param4">defines a moderately reliable region</param>
      /// <param name="param5">defines a slightly reliable region</param>
      [DllImport(CVAUX_LIBRARY)]
      public extern static void cvFindStereoCorrespondence(
         IntPtr leftImage, IntPtr rightImage,
         int mode, IntPtr depthImage,
         int maxDisparity,
         double param1, double param2, double param3,
         double param4, double param5);
   }
}